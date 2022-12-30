# RazorForms.Bootstrap5
## Bringing Bootstrap5 support to RazorForms

RazorForms is a package that simplifies creating form inputs in Razor Pages. **RazorForms.Bootstrap5** brings Bootstrap5 support to RazorForms.

RazorForms.Bootstrap5 v1.0.0 supports RazorForms v1.0.0 and Bootstrap ^5.0.

## Basic usage

To use, install both **RazorForms** and **RazorForms.Bootstrap5**.

### Usage with standard labels

Instead of calling `IServiceCollection.UseRazorForms()`, you call `IServiceCollection.UseRazorFormsWithBootstrap5()`. This has three overloads:

```csharp
// Sets up Bootstrap5 support with no customization. This is a good starting point to scaffold a simple project with Bootstrap5.

// Program.cs
builder.Services.UseRazorFormsWithBootstrap5();

// Sets up Bootstrap5 support with an Action<RazorFormsOptions>. This is probably how you'll use RazorForms.Bootstrap5.

// Program.cs
builder.Services.UseRazorFormsWithBootstrap5(o =>
{
    // customizations here
});

// Sets up Bootstrap5 support with an Action<T> where T : RazorFormsOptions. This allows you to use your own options class that extends RazorFormsOptions, for example if you create your own custom RazorForms tag helpers

// CustomOptions.cs
public class CustomOptions : RazorFormsOptions
{
    public ValidityAwareFormComponentOptions CustomTagHelperOptions { get; set; }
    // ...
}

// Program.cs
builder.Services.UseRazorFormsWithBootstrap5<CustomOptions>(o =>
{
    // customizations here
});
```

### Usage with floating labels

If you would rather use Bootstrap5 floating form labels, use the `IServiceCollection.UseRazorFormsWithBootstrap5FloatingLabels()` extension method. It has three overloads:

```csharp
// Sets up Bootstrap5 floating label support with no customization. This is a good starting point to scaffold a simple project with Bootstrap5.

// Program.cs
builder.Services.UseRazorFormsWithBootstrap5FloatingLabels();

// Sets up Bootstrap5 floating label support with an Action<RazorFormsOptions>. This is probably how you'll use RazorForms.Bootstrap5 with floating labels.

// Program.cs
builder.Services.UseRazorFormsWithBootstrap5FloatingLabels(o =>
{
    // customizations here
});

// Sets up Bootstrap5 floating label support with an Action<T> where T : RazorFormsOptions. This allows you to use your own options class that extends RazorFormsOptions, for example if you create your own custom RazorForms tag helpers

// CustomOptions.cs
public class CustomOptions : RazorFormsOptions
{
    public ValidityAwareFormComponentOptions CustomTagHelperOptions { get; set; }
    // ...
}

// Program.cs
builder.Services.UseRazorFormsWithBootstrap5FloatingLabels<CustomOptions>(o =>
{
    // customizations here
});
```