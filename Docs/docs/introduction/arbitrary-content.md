# Arbitrary content

Sometimes you want to be able to pass content into a tag helper. This section describes the different ways that you can do this with the various tag helpers.

## Add CSS classes to the `<div>` wrapper

If you want to add CSS classes to the `<div>` surrounding a tag helper's content, supply them as the value of the `class` attribute like normal:

```html
<form>
    <text-input asp-for="FirstName"
                class="some-other-class" />
</form>
```

The specified class names will be included on the `<div>` wrapper in the generated HTML. Passing class names to the generated child elements is not supported. Instead, use class name configuration to supply the appropriate classes to child elements, then pass a class to the wrapper `<div>` as described here. Use CSS descendant selectors to select the appropriate element, e.g. `.some-other-class .input-wrapper`.

## Pass arbitrary attributes to the underlying `<input>`

Any attribute other than `class` is automatically included in the generated `<input>` tag. For example, say you want users to answer a security question, but you don't want to decorate the answer field with `[DataType(DataType.Password)]` because semantically, it's not a password. In that case, you would need to manually supply `type="password"` in order to render an obscured input:

```html
<form>
    <text-input asp-for="SomeSecret"
                type="password">
        What is your mother's maiden name?
    </text-input>
</form>
```

## Pass HTML or other content to render as child content

If you want to use arbitrary HTML or other content within a tag helper, supply it as the child content of the supported tag helper:

```html
<form>
    <text-input asp-for="FirstName">
        Please provide your <span class="text-info">first name</span>
    </text-input>
    <check-input asp-for="AcceptsTos">
        I accept the <a asp-page="/terms">Terms of Use</a>
    </check-input>
    <button type="submit">
        Register
    </button>
</form>
```

**NOTE**: The render location varies depending on what type of content is rendered by the tag helper:

- For `<text-input>`, `<text-area-input>`, `<check-input>`, and `<radio-input>`, the supplied content is rendered as a child of the `<label>`.
- For `<check-input-group>` and `<radio-input-group>`, the supplied content is rendered after the `<label>` and before the error output.
- For `<select-input>`, the supplied content is rendered as a child of the `<select>`.