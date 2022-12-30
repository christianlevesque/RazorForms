# MarkupModel
Extends: (None)

The `MarkupModel` class is the base model used for rendering tag helper content to a template file.

## LabelHtml
Type: `Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput`

This property represents the HTML content of the `<label>`. It is not nullable because the `<label>` will never be null.

## InputHtml
Type: `Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput?`

This property represents the HTML content of the `<input>`. It is nullable because some tag helpers don't output an `<input>` (such as `<check-input-group>`) and some configurations will render the `<input>` as a child of the `<label>`.

## ElementOptions
Type: `object`

This property represents the configuration options for the tag helper. This is a copy of the configuration object from dependency injection made special for the template file during tag helper execution, so it is safe to mutate.

This property is an `object` because Razor templates can't have generic models. By making it an `object`, you can have use custom options classes without breaking built-in template files.