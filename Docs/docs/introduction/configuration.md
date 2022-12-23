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





## Applying your configuration options

Applying your Razor Forms configuration is simple. You have three options available:

1. Provide a new `RazorForms.Options.RazorFormsOptions` instance when registering RazorForms in your services.
2. Provide an `Action` that configures a default instance of `RazorForms.Options.RazorFormsOptions`.
3. Use one of the library-specific extension methods to use RazorForms preconfigured for one of several CSS libraries, such as Bootstrap 5.

**NOTE**: You never need to manually set the `TemplatePath` property on each option because `UseRazorForms(RazorFormsOptions)` sets these properties if they are empty, and every overload of `UseRazorForms()`, including library-specific extension methods, eventually calls back to `UseRazorForms(RazorFormsOptions)`. You only need to manually set the `TemplatePath` property if you wish to use a different template than the default.

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