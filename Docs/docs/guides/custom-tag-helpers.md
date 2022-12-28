# Creating custom tag helpers

RazorForms provides a decent amount of functionality out of the box, but it also enables you to easily create custom functionality. In this guide, we're going to create a custom tag helper that creates the [Materialize datepicker](https://materializecss.github.io/materialize/pickers.html).

We recommend you read up on [RazorForms internals](/docs/guides/razor-forms-internals) before trying to create custom tag helpers. That guide takes you through every execution step of every tag helper, to help give you an understanding of how RazorForms works. Creating custom tag helpers won't really make sense if you haven't read that guide.

To get started, create a new Razor Pages project and set up the layout file so that it includes the Materialize files as described in the [Materialize getting started guide](https://materializecss.github.io/materialize/getting-started.html). Make sure that you add tag helpers from whatever assembly contains your custom tag helper code (probably the Razor Pages project):

```cshtml
@* <razor-project-root>/Pages/_ViewImports.cshtml *@
@addTagHelper *, YourAssemblyName
```

## 1. Create options class

There are a couple different choices for creating your options class. The simplest solution is to create a subclass of [RazorFormsOptions](/docs/api/RazorFormsOptions) and then pass that to `services.UseRazorForms()`. You could also create your own POCO that contains your custom options and inject that into the DI container, and then request your custom class in your tag helper constructor. We're going to subclass [RazorFormsOptions](/docs/api/RazorFormsOptions) here. Create a new C# class named `MaterializeOptions` and subclass [RazorFormsOptions](/docs/api/RazorFormsOptions):

```csharp
using RazorForms.Options; // Import RazorFormsOptions

public class MaterializeOptions : RazorFormsOptions
{
    // Stay tuned!
}
```

Now, we want to create a property to contain the options for our date picker. For simplicity, we're going to use one of the RazorForms options classes, but you could also subclass those if you wanted to add custom options properties. A date field can probably be valid or invalid, right? So we're going to type our property to [ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions):

```csharp
using RazorForms.Options;

public class MaterializeOptions : RazorFormsOptions
{
	/// <summary>
	/// Represents the configuration options for the &lt;date-picker-input&gt; tag helper
	/// </summary>
	public ValidityAwareFormComponentOptions DatePickerInputOptions { get; set; } = new();
}
```

It's important to make sure that your options class has a default constructor because RazorForms creates a new instance of your options class if you use the overload of `UseRazorForms()` that accepts an `Action`. The compiler creates a default constructor for you if you don't have a constructor.

## 2. Map custom options to the options class

We have an options class to use now, but our custom options are all empty. We need to populate our options with values so our tag helper creates meaningful output.

### HTML and CSS requirements

Take a look at the [datepicker docs](https://materializecss.github.io/materialize/pickers.html). A brief review of the docs shows what sort of CSS and HTML structure we need:

- the datepicker `<input>` is surrounded by a `<div>` with class `input-field`
- the datepicker `<input>` and `<label>` have no individual wrappers
- the datepicker `<input>` comes before the `<label>`
- the datepicker uses a text input as its `<input>` element
- the datepicker `<input>` has a class of `datepicker`
- the datepicker `<label>` has no CSS class

Beyond these options for the datepicker HTML and CSS, we also need to decide what validation classes we want to add. If you look around the Materialize docs, you'll see there are a few utility classes that make sense to add:

- if the form element is valid, we should add the `valid` class to the `<input>`
- if the form element is valid, we should add the `green-text` class to the `<label>`
- if the form element is invalid, we should add the `invalid` class to the `<input>`
- if the form element is invalid, we should add the `red-text` class to the `<label>`
- the error `<li>` elements should have classes `helper-text` and `red-text`

Now we have *almost* everything decided that we need. We're only missing one piece: when you create an instance of [RazorFormsOptions](/docs/api/RazorFormsOptions) (including subclasses), RazorForms will automatically add the `TemplatePath` property to each options property built into RazorForms, but RazorForms doesn't use reflection to do this, so you need to supply your own `TemplatePath` in order for your tag helper to render. We aren't customizing the template for this tag helper, so since we're creating a validity-aware tag helper, we should use the validity-aware template. The path is `~/RazorFormsTemplates/Partials/ValidityAwareContent.cshtml`, or you can use the constant `Microsoft.Extensions.DependencyInjection.RazorFormsExtensions.ValidityAwareContentPartial`.

### Map requirements to options

So where do we map these values onto our options class? It depends on whether you want to pass an instance of `MaterializeOptions` into `UseRazorForms()` or pass an `Action<MaterializeOptions>` into `UseRazorForms()`. There's zero functional difference between the two choices, so I'll show you both here.

To pass an instance of `MaterializeOptions`:

```csharp
var options = new MaterializeOptions()
{
    DatePickerInputOptions = new ValidityAwareFormComponentOptions()
    {
        DatePickerInputOptions = new ()
        {
            TemplatePath = RazorFormsExtensions.ValidityAwareContentPartial,
            InputBlockWrapperClasses = "input-field",
            InputClasses = "datepicker",
            InputValidClasses = "valid",
            InputInvalidClasses = "invalid",
            LabelValidClasses = "green-text",
            LabelInvalidClasses = "red-text",
            ErrorClasses = "helper-text red-text",
            RemoveWrappers = true,
            InputFirst = true
        }
    };
}

builder.Services.UseRazorForms(options);
```

To pass an `Action<MaterializeOptions>`:

```csharp
builder.Services.UseRazorForms<MaterializeOptions>(o =>
{
    o.DatePickerInputOptions.TemplatePath = RazorFormsExtensions.ValidityAwareContentPartial;
    o.DatePickerInputOptions.InputBlockWrapperClasses = "input-field";
    o.DatePickerInputOptions.InputClasses = "datepicker";
    o.DatePickerInputOptions.InputValidClasses = "valid";
    o.DatePickerInputOptions.InputInvalidClasses = "invalid";
    o.DatePickerInputOptions.LabelValidClasses = "green-text";
    o.DatePickerInputOptions.LabelInvalidClasses = "red-text";
    o.DatePickerInputOptions.ErrorClasses = "helper-text red-text";
    o.DatePickerInputOptions.RemoveWrappers = true;
    o.DatePickerInputOptions.InputFirst = true;
});
```

## 3. Create tag helper class

All that's left to do now is create the class that actually represents the tag helper. Go ahead and create a new class called `DatePickerInputTagHelper` and put it anywhere in your project - Razor Pages will add it no matter where it is, as long as you added tag helpers from your project assembly at the start of this tutorial. Make `DatePickerInputTagHelper` a subclass of [ValidityAwareTagHelperBase<ValidityAwareFormComponentOptions>](/docs/guides/razor-forms-internals#razorforms.taghelpers.validityawaretaghelperbase) since our tag helper is validity-aware:

```csharp
using RazorForms.Options; // import ValidityAwareFormComponentOptions
using RazorForms.TagHelpers; // import ValidityAwareTagHelperBase

public class DatePickerInputTagHelper : ValidityAwareTagHelperBase<ValidityAwareFormComponentOptions>
{
}
```

### `ctor()`

We need to create a constructor. The base constructor requires three classes from DI: an `IHtmlGenerator`, and `IHtmlHelper`, and the options for this tag helper type. To get the options for this tag helper, we need to request our `MaterializeOptions` from DI and then pass its `DatePickerInputOptions` property into the base constructor:

```csharp
using RazorForms.Options; // import ValidityAwareFormComponentOptions
using RazorForms.TagHelpers; // import ValidityAwareTagHelperBase

public class DatePickerInputTagHelper : ValidityAwareTagHelperBase<ValidityAwareFormComponentOptions>
{
    public DatePickerInputTagHelper(
        IHtmlGenerator htmlGenerator,
        IHtmlHelper htmlHelper,
        MaterializeOptions options)
        : base(
            htmlGenerator,
            htmlHelper,
            options.DatePickerInputOptions)
    {
    }
}
```

Now we need to decide what [TagHelperBase properties](/docs/guides/razor-forms-internals#properties) we need to set here. We don't need to change the container tag or the input tag, and the `TemplatePath` property is meant to be used to override the template path in Razor code. `For` and `ViewContext` are set by the runtime. However, the `LabelReceivesChildContent` property looks promising. Do we want to be able to pass HTML into the tag helper to use as the `<label>`? I think we do. Besides, the `<input>` can't use the child content, and if the `<label>` doesn't receive the child content, the `<input>` will. (This makes sense for some input types like `<select>`, which can have child content.) In general, it's a good idea to set `LabelReceivesChildContent` to `true` unless the child content should explicitly be sent to the input element instead. So let's update our constructor:

```csharp
public DatePickerInputTagHelper(
    IHtmlGenerator htmlGenerator,
    IHtmlHelper htmlHelper,
    MaterializeOptions options)
    : base(
        htmlGenerator,
        htmlHelper,
        options.DatePickerInputOptions)
{
    LabelReceivesChildContent = true;
}
```

Now our constructor is done!

### Creating the `<input>`

Now, let's think about our `<input>`. As listed in our requirements above, we need an `<input type="text"/>`. To do that, we will need to override two methods: `CreateInputTagHelper()` and `AddCustomInputAttributes()`.

#### `CreateInputTagHelper()`

Our input is going to be an `InputTagHelper`, and it needs our `ViewContext` and `For` properties:

```csharp
protected override TagHelper CreateInputTagHelper()
{
    return new InputTagHelper(HtmlGenerator)
    {
        ViewContext = ViewContext,
        For = For
    };
}
```

This is good enough to get it working for now, but we're going to come back and make a change after we test our tag helper.

#### `AddCustomInputAttributes()`

Our input needs to have `type="text"` in its attributes. However, this is a problem because ASP.NET will automatically add `type="datetime-local"` when it sees that the model property is a `System.DateTime`. To prevent this, we need to manually add the `type="text"` to its attributes:

```csharp
protected override void AddCustomInputAttributes(TagHelperAttributeList attributes)
{
    attributes.Add("type", "text");
}
```

### Testing and changes

If you test your tag helper now, you'll see that it works just fine! Let's go ahead and create a small test form and model:

```cshtml
@page
@model TestModel

<form method="post">
    <date-picker-input asp-for="TestDate"/>
    <button type="submit"
            class="waves-effect waves-light btn">
        Submit
    </button>
</form>
```

```csharp
using System; // import DateTime
using Microsoft.AspNetCore.Mvc; // import BindPropertyAttribute
using Microsoft.AspNetCore.Mvc.RazorPages; // import PageModel

public class TestModel : PageModel
{
    [BindProperty]
    public DateTime? TestDate { get; set; }
}
```

Now, if you run your project and navigate to your page, you'll see a perfectly rendered Materialize datepicker. Well done!

There's just one small problem with this datepicker. To see that problem in action, remove the nullability of your `TestDate` property and set the default value to `DateTime.Now`:

```csharp
[BindProperty]
public DateTime TestDate { get; set; } = DateTime.Now;
```

Now re-run your project, and reload your page.

Yikes! That's really not useful for your potential users, is it?

This is a `System.DateTime` bound to a text input. ASP.NET is smart enough to handle this just fine, but the formatting leaves a lot to be desired - by default, the output is formatted to an ISO 8601 sortable date string. Fortunately, it's a simple fix to get a pretty date.

#### Fixing the date formatting on page load

Materialize sets the date format to `MMM dd, yyyy` by default. This is much prettier, so we'll go ahead and use that format too. To do that, we need to modify the `CreateInputTagHelper()` method and add a `Format` value to the `InputTagHelper`:

```csharp
protected override TagHelper CreateInputTagHelper()
{
    return new InputTagHelper(HtmlGenerator)
    {
        ViewContext = ViewContext,
        For = For,
        Format = "{0:MMM dd, yyyy}" // Same format used by Materialize
    };
}
```

Save the file, rebuild and rerun your project, and reload the page in your browser. Now, your date is formatted properly. Even better, if you change the date, it will have the *same format* as on page load.

Congratulations! You've created your first fully functional RazorForms tag helper.

## 4. (optional) Make tag helper more enduser-friendly

The tag helper now works perfectly as intended. However, the tag helper makes an assumption about the date formatting that can't be easily changed. If you're using this tag helper in your own app, that's not a huge deal, but if you want to distribute your tag helper as part of a NuGet package for your awesome new CSS framework, then you want your users to be able to change the date format to match their locale. To do that, we're going to change the type of the `MaterializeOptions.DatePickerInputOptions` property.

#### Create new options type

We still basically want the options to be [ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions), but we need to add a new property to hold our format. So create a new class named `FormattableOptions` that subclasses [ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions), and add a `string? Format` property:

```csharp
public class FormattableOptions : ValidityAwareFormComponentOptions
{
	/// <summary>
	/// A default value to use for the <c>asp-format</c> attribute if none is specified
	/// </summary>
	public string? Format { get; set; }
}
```

Next, update the type of `MaterializeOptions.DatePickerInputOptions` to be `FormattableOptions`:

```csharp
public class MaterializeOptions : RazorFormsOptions
{
	/// <summary>
	/// Represents the configuration options for the &lt;date-picker-input&gt; tag helper
	/// </summary>
	public FormattableOptions DatePickerInputOptions { get; set; } = new();
}
```

Now we can update the generic type parameter to [ValidityAwareTagHelperBase](/docs/guides/razor-forms-internals) when defining `DatePickerInputTagHelper`:

```csharp
public class DatePickerInputTagHelper : ValidityAwareTagHelperBase<FormattableOptions>
{
    // ...
}
```

And finally, we can add the format to our `CreateInputTagHelper()` override:

```csharp
protected override TagHelper CreateInputTagHelper()
{
    return new InputTagHelper(HtmlGenerator)
    {
        ViewContext = ViewContext,
        For = For,
        Format = Options.Format
    };
}
```

#### Add a per-instance format override

It's all well and good to let the developer create a global option for the format specifier. But what if the developer wants to override the format for a single instance? Fortunately, this is very simple. Start by adding a new `string? Format` property with the following attribute:

```csharp
public class DatePickerInputTagHelper : ValidityAwareTagHelperBase<FormattableOptions>
{
    [HtmlAttributeName("asp-format")]
    public string? Format { get; set; }

    // ...
}
```

This lets your developers specify a format by passing the `asp-format` attribute, just like with the `InputTagHelper`. Now, all we need to do is tell the tag helper to use `Format` if it's specified, and if not, fall back to the global option value:

```csharp
protected override TagHelper CreateInputTagHelper()
{
    return new InputTagHelper(HtmlGenerator)
    {
        ViewContext = ViewContext,
        For = For,
        Format = Format ?? Options.Format
    };
}
```

That's all there is to it! Now you have a functional tag helper with custom configuration and an overridable tag helper attribute.