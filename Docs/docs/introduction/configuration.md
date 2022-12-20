# Configuration

RazorForms tag helpers are highly configurable. The configuration options available depend on whether the tag helper outputs validation-aware information or not.

**Tag helpers WITHOUT validation information** are `<check-input>` and `<radio-input>`. These use the `RazorForms.Options.FormComponentOptions` configuration class.

**Tag helpers WITH validation information** are `<text-input>`, `<text-area-input>`, `<select-input>`, `<check-input-group>`, and `<radio-input-group>`. These use the `RazorForms.Options.ValidityAwareFormComponentOptions` configuration class, which extends `RazorForms.Options.FormComponentOptions`.

## Configuring RazorForms

RazorForms configuration is all stored on a single instance of `RazorForms.Options.RazorFormsOptions`. This class has an options property for each tag helper supported by RazorForms.

For tag helpers without validation support (`<check-input>` and `<radio-input>`), these options properties are of type `RazorForms.Options.FormComponentOptions`.

For tag helpers with validation support (`<text-input>`, `<text-area-input>`, `<select-input>`, `<check-input-group>`, and `<radio-input-group>`), these options properties are of type `RazorForms.Options.ValidityAwareFormComponentOptions`.

To configure RazorForms completely from scratch, you can use the following configuration object:

```csharp
using Razorforms.Options; // import options classes

var config = new RazorFormsOptions
{
    CheckInputOptions = new FormComponentOptions(), // configures <check-input>
    RadioInputOptions = new FormComponentOptions(), // configures <radio-input>
    CheckInputGroupOptions = new ValidityAwareFormComponentOptions(), // configures <check-input-group>
    RadioInputGroupOptions = new ValidityAwareFormComponentOptions(), // configures <radio-input-group>
    TextInputOptions = new ValidityAwareFormComponentOptions(), // configures <text-input>
    TextAreaInputOptions = new ValidityAwareFormComponentOptions(), // configures <text-area-input>
    SelectInputOptions = new ValidityAwareFormComponentOptions() // configures <select-input>
};
```

**NOTE**: To apply your configuration options to RazorForms, see [applying your configuration options](#applying-your-configuration-options)

### FormComponentOptions properties

The `FormComponentOptions` class is used to configure all tag helpers in RazorForms (the `ValidityAwareFormComponentOptions` class extends this, and all of these properties are also used for validity-aware tag helpers). Most of these properties define CSS classes to apply to various parts of the markup. To reference the markup created by each tag helper, view that tag helper's documentation page.

The following properties are used:

#### `string TemplatePath`

The `TemplatePath` property specifies the path to the `.cshtml` file to use as a template when rendering the tag helper. This path doesn't include the leading `~/` or the `.cshtml` extension, so if the template you want to use resides at `~/Views/RazorForms/TextInput.cshtml`, you would set this property to `Views/RazorForms/TextInput`.

#### `string ComponentWrapperClasses`

The `ComponentWrapperClasses` property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the tag helper output.

#### `string LabelWrapperClasses`

The `LabelWrapperClasses` property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<label>`.

#### `string LabelClasses`

The `LabelClasses` property specifies any CSS classes (space-separated) that should be applied to the `<label>`.

#### `string LabelTextHtmlWrapper`

The `LabelTextHtmlWrapper` property specifies what HTML tag to wrap the `<label>` text with. For example, the Materialize CSS library expects checkbox label text to be surrounded by a `<span>`, so this value would be set to `span`.

#### `string InputWrapperClasses`

The `InputWrapperClasses` property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<input>`.

#### `string InputClasses`

The `InputClasses` property specifies any CSS classes (space-separated) that should be applied to the `<input>`.

#### `bool RemoveWrappers`

The `RemoveWrappers` property indicates whether or not the generated markup should include a `<div>` wrapper around the `<label>` and the `<input>`. If `true`, these wrappers are removed, and the `<label>` and `<input>` tags are rendered adjacent to one another.

#### `bool InputFirst`

The `InputFirst` property indicates whether or not the `<input>` should be rendered first. By default, the `<label>` is rendered before the `<input>`, but for some tag helpers, the `<input>` should be rendered before the `<label>`. For example, in Bootstrap, the `<input>` needs to be rendered first in checkboxes, radios, and if using floating form labels.

#### `bool RenderInputInsideLabel`

The `RenderInputInsideLabel` property indicates whether or not the generated markup should render the `<input>` inside the `<label>`. Some design systems expect this architecture, e.g., the Materialize CSS library's styles for checkboxes.

### ValidityAwareFormComponentOptions properties

The `ValidityAwareFormComponentOptions` class is used to provide additional configuration for tag helpers that include validation information. This class extends `FormComponentOptions`, so all of `FormComponentOptions` properties apply to tag helpers that use `ValidityAwareFormComponentOptions`. Most of the properties add classes to the different markup sections based on the validity of the input.

CSS class-providing properties whose names include `Valid` are applied if the input is **explicitly valid**, i.e. `ModelState.GetFieldValidationState() == ModelValidationState.Valid`. If the input is invalid, skipped, or not yet validated, these classes are not applied.

CSS class-providing properties whose names include `Invalid` are applied if the input is **explicitly invalid**, i.e. `ModelState.GetFieldValidationState() == ModelValidationState.Invalid`. If the input is valid, skipped, or not yet validated, these classes are not applied.

The following properties are used:

#### `string ComponentWrapperValidClasses`, `string ComponentWrapperInvalidClasses`

These properties specify any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the tag helper output when the input is valid or invalid, respectively.

#### `string InputBlockWrapperClasses`

The `InputBlockWrapperClasses` property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the input block of the tag helper output.

#### `string InputBlockWrapperValidClasses`, `string InputBlockWrapperInvalidClasses`

These properties specify any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the input block when the input is valid or invalid, respectively.

#### `string LabelWrapperValidClasses`, `string LabelWrapperInvalidClasses`

These properties specify any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<label>` when the input is valid or invalid, respectively.

#### `string LabelValidClasses`, `string LabelInvalidClasses`

These properties specify any CSS classes (space-separated) that should be applied to the `<label>` when the input is valid or invalid, respectively.

#### `string InputWrapperValidClasses`, `string InputWrapperInvalidClasses`

These properties specify any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<input>` when the input is valid or invalid, respectively.

#### `string InputValidClasses`, `string InputInvalidClasses`

These properties specify any CSS classes (space-separated) that should be applied to the `<input>` when the input is valid or invalid, respectively.

#### `string ErrorWrapperClasses`

The `ErrorWrapperClasses` property specifies any CSS classes (space-separated) that should be applied to the `<ul>` that contains validation error messages. Because this block is only intended to display error information, there are no validation state-specific classes.

#### `string ErrorClasses`

The `ErrorClasses` property specifies any CSS classes (space-separated) that should be applied to the `<li>`s that contain individual validation error messages. Because this element is only intended to display error information, there are no validation state-specific classes.

#### `bool AlwaysRenderErrorContainer`

This property specifies whether the `<ul>` that contains validation error messages should always be rendered. If `true`, the `<ul>` will be rendered even if there are no validation messages. This can be helpful if you intend to perform client-side form validation; by ensuring the `<ul>` is always rendered, your JavaScript can skip creating the error message block and jump straight to populating it with errors.

## Applying your configuration options

Applying your Razor Forms configuration is simple. You have three options available:

1. Provide a new `RazorForms.Options.RazorFormsOptions` instance when registering RazorForms in your services.
2. Provide an `Action` that configures a default instance of `RazorForms.Options.RazorFormsOptions`.
3. Use one of the library-specific extension methods to use RazorForms preconfigured for one of several CSS libraries, such as Bootstrap 5.

### Configuring RazorForms with a new `RazorFormsOptions` instance

To provide your own `RazorFormsOptions` instance, pass it to your `UseRazorForms()` call:

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorForms.Options; // import RazorFormsOptions

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

var razorFormsOptions = new RazorFormsOptions
{
    // Add your customizations here
};

builder.Services.UseRazorForms(razorFormsOptions); // Add your customized options to RazorForms
```

**NOTE**: When providing your own `RazorFormsOptions` instance, you need to manually set the `TemplatePath` property on each option because options class properties default to CLR defaults and empty strings only. RazorForms requires a `.cshtml` template for each tag helper. See [TemplatePath](#template-path) for more information.

### Configuring RazorForms with an `Action`

To change values on the default `RazorFormsOptions` instance, pass an `Action<RazorFormsOptions>` to your `UseRazorForms()` call:

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Services.UseRazorForms(o =>
{
    // Add your customizations here
});
```

**NOTE**: When configuring a `RazorFormsOptions` instance with an `Action`, the default template paths are supplied for you. So if the default templates are acceptable to you, there's no need to set the `TemplatePath` property on each option.

### Configuring RazorForms to use a preconfigured CSS library

When using a preconfigured CSS library, you have two options: Use the library as-is, or pass an `Action<RazorFormsOptions>` to provide additional configuration.

**NOTE**: RazorForms does **not** include third-party library CSS code! All RazorForms does is set up third-party CSS class names in the rendered markup. You will still need to include the CSS files in whatever way you choose.

We currently support the following libraries out of the box:

#### Bootstrap 5

[Bootstrap 5](https://getboostrap.com) is currently supported with two different configurations: standard labels and floating labels.

##### Standard labels

Standard labels appear before the input on a separate line. To use standard labels:

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// As-is (not recommended)
builder.Services.UseRazorFormsWithBootstrap5();

// With additional configuration (recommended)
builder.Services.UseRazorFormsWithBootstrap5(o =>
{
    // Add your customizations here
});
```

##### Floating labels

Floating labels appear inside the input, then "float" above the input when the input receives focus. To use floating labels:

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// As-is (not recommended)
builder.Services.UseRazorFormsWithBootstrap5FloatingLabels();

// With additional configuration (recommended)
builder.Services.UseRazorFormsWithBootstrap5FloatingLabels(o =>
{
    // Add your customizations here
});
```

#### Bulma

[Bulma](https://bulma.io) is currently supported with the standard form configuration.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// As-is (not recommended)
builder.Services.UseRazorFormsWithBulma();

// With additional configuration (recommended)
builder.Services.UseRazorFormsWithBulma(o =>
{
    // Add your customizations here
});
```

#### Materialize

[Materialize](https://materializecss.com/) is currently supported with the standard form configuration.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// As-is (not recommended)
builder.Services.UseRazorFormsWithMaterialize();

// With additional configuration (recommended)
builder.Services.UseRazorFormsWithMaterialize(o =>
{
    // Add your customizations here
});
```