# RazorForms.Materialize
## Bringing MaterializeCSS support to RazorForms

RazorForms is a package that simplifies creating form inputs in Razor Pages. **RazorForms.Materialize** brings MaterializeCSS support to RazorForms.

RazorForms.Materialize v1.0.0 supports RazorForms v1.0.0 and MaterializeCSS ^1.0.

### Basic usage

To use, install both **RazorForms** and **RazorForms.Materialize**. Then, instead of calling `IServiceCollection.UseRazorForms()`, you call `IServiceCollection.UseRazorFormsWithMaterialize()`. This has three overloads:

```csharp
// Sets up Materialize support with no customization. This is a good starting point to scaffold a simple project with MaterializeCSS.

// Program.cs
builder.Services.UseRazorFormsWithMaterialize();

// Sets up Materialize support with an Action<MaterializeOptions>. This is probably how you'll use RazorForms.Materialize.

// Program.cs
builder.Services.UseRazorFormsWithMaterialize(o =>
{
    // customizations here
});

// Sets up materialize support with an Action<T> where T : MaterializeOptions. This allows you to use your own options class that extends MaterializeOptions, for example if you create your own custom RazorForms tag helpers

// CustomOptions.cs
public class CustomOptions : MaterializeOptions
{
    public ValidityAwareFormComponentOptions CustomTagHelperOptions { get; set; }
    // ...
}

// Program.cs
builder.Services.UseRazorFormsWithMaterialize<CustomOptions>(o =>
{
    // customizations here
});
```

### Tag helpers

**RazorForms.Materialize** adds two new tag helpers on top of the **RazorForms** tag helper library.

#### `<date-picker-input>`

The `<date-picker-input>` tag helper renders the [MaterializeCSS datepicker](https://materializecss.github.io/materialize/pickers.html). It's meant to be used with a `System.DateTime` as its model member, but it should also work with a properly formatted `string`.

```csharp
// ExampleModel.cs

using System; // import DateTime
using Microsoft.AspNetCore.Mvc; // import BindPropertyAttribute
using Microsoft.AspNetCore.Mvc.RazorPages; // import PageModel

public class ExampleModel : PageModel
{
    [BindProperty]
    public DateTime TestDate { get; set; }
}
```

```cshtml
@* ExamplePage.cshtml *@
@model ExampleModel

<form method="post">
    <date-picker-input asp-for="TestDate"/>
    @* ... *@
</form>
```

By default, the date is formatted as `MMM dd, yyyy` (e.g., Nov 5, 1955). This matches the default of MaterializeCSS. You can change this for an individual instance by using the `asp-format` attribute, just like with the built-in input tag helper:

```cshtml
@* ExamplePage.cshtml *@
@model ExampleModel

<form method="post">
    <date-picker-input asp-for="TestDate" asp-format="{0:d}"/>
    @* ... *@
</form>
```

Alternatively, you can change this globally by changing the `MaterializeOptions` configuration:

```csharp
// Program.cs
builder.Services.UseRazorFormsWithMaterialize(o =>
{
    o.DatePickerInputOptions.Format = "{0:d}";
});
```

#### `<time-picker-input>`

The `<time-picker-input>` tag helper renders the [MaterializeCSS timepicker](https://materializecss.github.io/materialize/pickers.html). It's meant to be used with a `System.DateTime` as its model member, but it should also work with a properly formatted `string`.

```csharp
// ExampleModel.cs

using System; // import DateTime
using Microsoft.AspNetCore.Mvc; // import BindPropertyAttribute
using Microsoft.AspNetCore.Mvc.RazorPages; // import PageModel

public class ExampleModel : PageModel
{
    [BindProperty]
    public DateTime TestTime { get; set; }
}
```

```cshtml
@* ExamplePage.cshtml *@
@model ExampleModel

<form method="post">
    <time-picker-input asp-for="TestTime"/>
    @* ... *@
</form>
```

By default, the time is formatted as `t` (e.g., 12:45 PM). This matches the default of MaterializeCSS. You can change this for an individual instance by using the `asp-format` attribute, just like with the built-in input tag helper:

```cshtml
@* ExamplePage.cshtml *@
@model ExampleModel

<form method="post">
    <time-picker-input asp-for="TestDate" asp-format="{0:T}"/>
    @* ... *@
</form>
```

Alternatively, you can change this globally by changing the `MaterializeOptions` configuration:

```csharp
// Program.cs
builder.Services.UseRazorFormsWithMaterialize(o =>
{
    o.TimePickerInputOptions.Format = "{0:T}";
});
```