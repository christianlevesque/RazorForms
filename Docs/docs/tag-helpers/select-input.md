# \<select-input>

The `<select-input>` tag helper is used to create `<select>` elements.

The `<select-input>` tag helper **generates error messages** if its corresponding model member fails validation.

The `<select-input>` tag helper **renders child content** as a raw HTML child of the generated `<select>`.

## Usage notes

### Basic usage

To use the `<select-input>` tag helper, all you need to do is add it to the form and supply the `asp-for` attribute, just like you would with the built-in `<select>` tag helper.

Because `<select>` elements expect child options, you can supply them as child content to the `<select-input>` tag helper:

```csharp
public class MyModel : PageModel
{
    [BindProperty]
    public string FavoriteBookSeries { get; set; }
}
```

```cshtml
@model MyModel

<form>
    <select-input asp-for="FavoriteBookSeries">
        <option value="Lord of the Rings">The Lord of the Rings</option>
        <option value="Wheel of Time">The Wheel of Time</option>
        <option value="Harry Potter">Harry Potter</option>
    </select-input>
</form>
```

### Child content

### Programmatic option rendering

In ASP.NET, it's possible to render options for a `<select>` by supplying the `asp-items` attribute with a `IEnumerable<SelectListItem>`. This is also possible in RazorForms. The previous example could be rewritten:

```csharp
public class MyModel : PageModel
{
    [BindProperty]
    public string FavoriteBookSeries { get; set; }

    public IEnumerable<SelectListItem> FavoriteBookOptions = 
    {
        new ()
        {
            Value = "Lord of the Rings",
            Text = "The Lord of the Rings"
        },
        new ()
        {
            Value = "Wheel of Time",
            Text = "The Wheel of Time"
        },
        new ()
        {
            Value = "Harry Potter",
            Text = "Harry Potter"
        }
    };
}
```

```cshtml
@model MyModel

<form>
    <select-input asp-for="FavoriteBookSeries"
                  asp-items="Model.FavoriteBookOptions"/>
</form>
```

For more information on the `SelectListItem` class, see [SelectListItem on the Microsoft docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.rendering.selectlistitem?view=aspnetcore-6.0).

## Generated HTML

The HTML generated by the `<select-input>` tag helper looks like this:

```html
<div> <!-- component wrapper -->
    <div> <!-- input block wrapper -->
        <div> <!-- label wrapper, disable in configuration -->
            <label for="FavoriteBookSeries">
                Favorite book series
            </label>
        </div>

        <div> <!-- select wrapper, disable in configuration -->
            <select id="FavoriteBookSeries"
                    name="FavoriteBookSeries">
                <option>...</option>
            </select>
        </div>
    </div>

    <ul> <!-- error wrapper, only rendered if errors are present -->
        <li>...</li>
    </ul>
</div>
```