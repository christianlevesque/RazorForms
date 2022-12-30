# Configuration

RazorForms tag helpers are highly configurable. The configuration options available depend on whether the tag helper outputs validation-aware information or not.

**Tag helpers WITHOUT validation information** are `<check-input>` and `<radio-input>`. These use the [RazorForms.Options.FormComponentOptions](/docs/api/FormComponentOptions) configuration class.

**Tag helpers WITH validation information** are `<text-input>`, `<text-area-input>`, `<select-input>`, `<check-input-group>`, and `<radio-input-group>`. These use the [RazorForms.Options.ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions) configuration class, which extends `RazorForms.Options.FormComponentOptions`.

## Configuring RazorForms

RazorForms configuration is all stored on a single instance of [RazorForms.Options.RazorFormsOptions](/docs/api/RazorFormsOptions). This class has an options property for each tag helper supported by RazorForms.

For tag helpers without validation support (`<check-input>` and `<radio-input>`), these options properties are of type [RazorForms.Options.FormComponentOptions](/docs/api/FormComponentOptions).

For tag helpers with validation support (`<text-input>`, `<text-area-input>`, `<select-input>`, `<check-input-group>`, and `<radio-input-group>`), these options properties are of type [RazorForms.Options.ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions).

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

1. Provide a new [RazorForms.Options.RazorFormsOptions](/docs/api/RazorFormsOptions) instance when registering RazorForms in your services.
2. Provide an `Action` that configures a default instance of [RazorForms.Options.RazorFormsOptions](/docs/api/RazorFormsOptions).
3. Use one of the library-specific extension methods to use RazorForms preconfigured for one of several CSS libraries, such as Bootstrap 5. (You'll need to install additional NuGet packages to enable these libraries.)

**NOTE**: You never need to manually set the `TemplatePath` property on each option because `UseRazorForms(RazorFormsOptions)` sets these properties if they are empty, and every overload of `UseRazorForms()`, including library-specific extension methods, eventually calls back to `UseRazorForms(RazorFormsOptions)`. You only need to manually set the `TemplatePath` property if you wish to use a different template than the default, or if you add configurations for custom tag helpers.

### Configuring RazorForms with a new `RazorFormsOptions` instance

To provide your own [RazorFormsOptions](/docs/api/RazorFormsOptions) instance, pass it to your `UseRazorForms()` call:

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

To change values on the default [RazorFormsOptions](/docs/api/RazorFormsOptions) instance, pass an `Action<RazorFormsOptions>` to your `UseRazorForms()` call:

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

### Configuring RazorForms with a subclass of [RazorFormsOptions](/docs/api/RazorFormsOptions)

You may want to extend [RazorFormsOptions](/docs/api/RazorFormsOptions). This will allow you to define your configuration values in their own class file, or even create additional options for [custom tag helpers](/docs/guides/custom-tag-helpers). You can register your configuration using a custom options class similarly to how you register your configuration with a [RazorFormsOptions](/docs/api/RazorFormsOptions).

#### Configuring RazorForms with your subclassed options

The overload of `UseRazorForms()` that accepts an instance of an options object is actually generic: `UseRazorForms<T>(T options) where T : RazorFormsOptions`. So all you need to do is create your instance and pass it in as before:

```csharp
// CustomOptions.cs
public class CustomOptions : RazorFormsOptions
{
    // Customizations here
}

// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

var options = new CustomOptions();

builder.Services.UseRazorForms(options);
```

#### Configuring RazorForms with your subclassed options and an `Action`

There is a third overload of `UseRazorForms()` that takes a type parameter `T` and an `Action<T>`:

```csharp
// CustomOptions.cs
public class CustomOptions : RazorFormsOptions
{
    // Customizations here
}

// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Services.UseRazorForms<CustomOptions>(o =>
{
    // Add your customizations here
});
```

### Configuring RazorForms to use a preconfigured CSS library

You can also use RazorForms with the following CSS libraries by installing additional NuGet packages:

- Bootstrap5 via [RazorForms.Bootstrap5](https://www.nuget.org/packages/RazorForms.Bootstrap5)
- Materialize via [RazorForms.Materialize](https://www.nuget.org/packages/RazorForms.Materialize)
- Bulma via [RazorForms.Bulma](https://www.nuget.org/packages/RazorForms.Bulma)