# FormComponentOptions
Extends: (None)

The `FormComponentOptions` class is the base class used to configure all tag helpers in RazorForms. Most of its properties define CSS classes to apply to various parts of the markup. To reference the default markup created by each tag helper, view that tag helper's documentation page, or view our guide on [tag helper output structure](/docs/guides/output-structure)

## TemplatePath
Type: `string`

This property specifies the path to the `.cshtml` file to use as a template when rendering the tag helper. If this is not supplied, RazorForms will automatically set its own `TemplatePath`.

## ComponentWrapperClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the tag helper output.

## LabelWrapperClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<label>`.

## LabelClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<label>`.

## LabelTextHtmlWrapper
Type: `string`

This property specifies what HTML tag to wrap the `<label>` text with. For example, the Materialize CSS library expects checkbox label text to be surrounded by a `<span>`, so this value would be set to `span`.

## InputWrapperClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<input>`.

## InputClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<input>`.

## RemoveWrappers
Type: `bool`

This property indicates whether or not the generated markup should include a `<div>` wrapper around the `<label>` and the `<input>`. If `true`, these wrappers are removed, and the `<label>` and `<input>` tags are rendered adjacent to one another.

## InputFirst
Type: `bool`

This property indicates whether or not the `<input>` should be rendered first. By default, the `<label>` is rendered before the `<input>`, but for some tag helpers, the `<input>` should be rendered before the `<label>`. For example, in Bootstrap, the `<input>` needs to be rendered first in checkboxes, radios, and if using floating form labels.

## RenderInputInsideLabel
Type: `bool`

This property indicates whether or not the generated markup should render the `<input>` inside the `<label>`. Some design systems expect this architecture, e.g., the Materialize CSS library's styles for checkboxes.