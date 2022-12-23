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