# RazorForms.Bulma
## Bringing Bulma support to RazorForms

RazorForms is a package that simplifies creating form inputs in Razor Pages. **RazorForms.Bulma** brings Bulma support to RazorForms.

RazorForms.Bulma v1.0.0 supports RazorForms v1.0.0 and Bulma ^1.0.

## Basic usage

To use, install both **RazorForms** and **RazorForms.Bulma**. Instead of calling `IServiceCollection.UseRazorForms()`, you call `IServiceCollection.UseRazorFormsWithBulma()`. This has three overloads:

```csharp
// Sets up Bulma support with no customization. This is a good starting point to scaffold a simple project with Bulma.

// Program.cs
builder.Services.UseRazorFormsWithBulma();

// Sets up Bulma support with an Action<RazorFormsOptions>. This is probably how you'll use RazorForms.Bulma.

// Program.cs
builder.Services.UseRazorFormsWithBulma(o =>
{
    // customizations here
});

// Sets up Bulma support with an Action<T> where T : RazorFormsOptions. This allows you to use your own options class that extends RazorFormsOptions, for example if you create your own custom RazorForms tag helpers

// CustomOptions.cs
public class CustomOptions : RazorFormsOptions
{
    public ValidityAwareFormComponentOptions CustomTagHelperOptions { get; set; }
    // ...
}

// Program.cs
builder.Services.UseRazorFormsWithBulma<CustomOptions>(o =>
{
    // customizations here
});
```