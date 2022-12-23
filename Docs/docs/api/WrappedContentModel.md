# WrappedContentModel
Extends: (None)

The `WrappedContentModel` class is used to pass data to the `~/RazorFormsTemplates/Partials/WrappedContent.cshtml` partial, which is used to optionally wrap `<label>` and `<input>` tags in a `<div>`.

## Properties

### Content
Type: `Microsoft.AspNetCore.Html.IHtmlContent?`

This property represents the HTML content of the content to wrap in a `<div>`. It may be null; if so, the `~/RazorFormsTemplates/Partials/WrappedContent.cshtml` template will not render any output.

### WrapperClasses
Type: `string`

This property represents the CSS classes, if any, to apply to the `<div>` wrapper.

### RemoveWrappers
Type: `bool`

This property represents whether to remove the `<div>` wrapper and render the `Content` contents directly.

## Constructors

### ctor(IHtmlContent? content, string wrapperClasses, bool removeWrappers)
