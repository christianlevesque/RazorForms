# Razor Forms internals

To give you a better idea of how Razor Forms actually does what it does, let's take a look under the hood at the class that does all the work for every single tag helper in the Razor Forms library: `RazorForms.TagHelpers.TagHelperBase`. You can view the source code [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/TagHelperBase.cs).

## ProcessAsync()

Like all tag helpers, everything starts at `ProcessAsync()`. None of the RazorForms tag helpers override `ProcessAsync()`, so the implementation in `TagHelperBase` is what runs in every tag helper. It only has 7 lines of code; these 7 statements hook into extensible parts of RazorForms to generate all the different tag helpers available. `ProcessAsync()` is broken down into 3 logical tasks: configure the tag helper output, set up the model for the `.cshtml` template, and render the `.cshtml` template to the tag helper output. The code for `ProcessAsync()` looks like this:

```csharp
public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
{
    // Set up the output wrapper
    output.TagName = ContainerTag;
    output.TagMode = ContainerTagMode;

    // Set up model
    var model = await GenerateHtmlModel(context, output);
    await ProcessModel(model);

    // Render content
    var content = await HtmlHelper.PartialAsync(
        TemplatePath ?? Options.TemplatePath,
        model);
    output.Content.SetHtmlContent(content);
}
```

### Configure the tag helper output

Configuring the tag helper output is simple. All we do is set the `TagName` and `TagMode` based on protected properties on `TagHelperBase`. We use protected properties because these are implementation details of the tag helpers themselves, and don't change based on your RazorForms configuration - so it's not appropriate to give end users control over these properties or store them in configuration.

`ContainerTag` defaults to an empty string, so by default, there is no HTML wrapper around the tag helper output.

`ContainerTag` defaults to `TagMode.StartTagAndEndTag`, which is what you want if you're going to render child input inside.

### Set up the template model

All RazorForms `.cshtml` templates have a POCO model, so we need to set that up here.

The `GenerateHtmlModel()` method creates the model. This method is one of only two methods on TagHelperBase that aren't `virtual`, but this method calls several other methods that *are* virtual, so you can still customize this process.

The `ProcessModel()` method allows you to arbitrarily modify the model in whatever way you want. So this would be a prime opportunity to do custom processing for your model (for example, deciding what icon to render based on the validity of the data model).

### Render the template to the tag helper output

Now that the model is created and processed, we can render the template and write its output.

First, we need to prepare the `HtmlHelper`, which is a class instance of `IHtmlHelper`. It's not ready to use out of the box - it needs to be contextualized first, which gives it access to the `ViewContext` that it needs to do its job. It doesn't come pre-contextualized because the underlying class, `HtmlHelper`, is registered in DI as a transient service, so since you're setting this one up yourself, you have to manually prepare it for use.

Next, we render the template using our contextualized `HtmlHelper`. `TagHelperBase` will use the `TemplatePath` value if provided, or else it will fall back to the `TemplatePath` on the configuration options for that tag helper (see the [custom templates guide](/docs/guides/custom-templates#use-a-custom-template-for-a-single-instance-of-a-tag-helper)) for more information.

Then, all we need to do is set the content of the tag helper output to our rendered template!

## GenerateHtmlModel()

It seems like we got a lot for just a little in `ProcessAsync()`, doesn't it? Sure, a lot of the markup comes from the template, but most of the logic happens in the tag helper, so where is it?

Well, as it turns out, most of this happens in `GenerateHtmlModel()` and the methods it calls. Each line of the source is a different logical step, so after we read the source, we'll go over it sequentially:

```csharp
protected async Task<TModel> GenerateHtmlModel(TagHelperContext context, TagHelperOutput output)
{
    // Set up the viewmodel to send to the Razor template
    var model = new TModel
    {
        LabelHtml = await CreateLabel(context, output),
        ElementOptions = new TOptions()
    };

    if (!Options.RenderInputInsideLabel)
    {
        model.InputHtml = await CreateInputCore(context, output);
    }

    SetupModelOptions(model.ElementOptions);
    AddCssClasses(model, output.Attributes);

    return model;
}
```

### Create `<label>` output

The `<label>` and the `<input>` tags aren't created in the Razor template. Instead, they're created here, in C# code. But why?

The `<label>` and `<input>` tag helpers use the `asp-for` parameter to decide which model member they're rendering for. `asp-for` is a [ModelExpression](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.viewfeatures.modelexpression?view=aspnetcore-7.0), which is a little different from most other expressions. For starters, it isn't representable as a lambda: when you use a `ModelExpression` in a Razor template, it infers that the member you reference is coming off your template's model. There's no straightforward way to deconstruct a `ModelExpression` into its underlying model reference, so there's also no straightforward way to create a reusable Razor template when form inputs are involved. If you try, what happens is your `ModelExpression` will no longer represent your Razor Page model member - instead, in your reusable Razor template, your `ModelExpression` will point to the `ModelExpression` property on your ViewComponent, and none of it will work as expected.

RazorForms sidesteps this problem by creating the `<label>` and `<input>` tag helpers manually in C# code. By working in C# instead of Razor, the `ModelExpression` resolves as expected, and everything just works. So for this reason, the rendered `<label>` and `<input>` are stored on the model passed to the Razor template, and the Razor template just injects those values directly into the template from the model properties.

### Create new `TOptions`

The `TOptions` here represents the type of the configuration options for this tag helper. For the built-in tag helpers, validity-aware tag helpers use [ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions) and validity-unaware tag helpers use [FormComponentOptions](/docs/api/FormComponentOptions). But if we already have the options object passed into the tag helper, why are we creating a new one here?

In order to keep the Razor template logic as minimal as possible, we want as much processing as possible to happen in the C# code of the tag helper. To do that, we actually calculate the CSS classes that need to be applied to the output in the tag helper, then pass those to the `TOptions`