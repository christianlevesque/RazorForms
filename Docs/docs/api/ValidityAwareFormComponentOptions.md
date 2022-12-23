# ValidityAwareFormComponentOptions properties
Extends: [RazorForms.Options.FormComponentOptions](/docs/api/FormComponentOptions)

The `ValidityAwareFormComponentOptions` class is used to provide additional configuration for tag helpers that include validation information. Most of the properties add classes to the different markup sections based on the validity of the input. To reference the default markup created by each tag helper, view that tag helper's documentation page, or view our guide on [tag helper output structure](/docs/guides/output-structure)

CSS class-providing properties whose names include `Valid` are applied if the input is **explicitly valid**, i.e. `ModelState.GetFieldValidationState("PropertyName") == ModelValidationState.Valid`. If the input is invalid, skipped, or not yet validated, these classes are not applied.

CSS class-providing properties whose names include `Invalid` are applied if the input is **explicitly invalid**, i.e. `ModelState.GetFieldValidationState("PropertyName") == ModelValidationState.Invalid`. If the input is valid, skipped, or not yet validated, these classes are not applied.

## ComponentWrapperValidClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the tag helper output **when the model member is in a valid state**.

## ComponentWrapperInvalidClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the tag helper output **when the model member is in an invalid state**.

## InputBlockWrapperClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the input block of the tag helper output.

## InputBlockWrapperValidClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the input block of the tag helper output **when the model member is in a valid state**.

## InputBlockWrapperInvalidClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the input block of the tag helper output **when the model member is in an invalid state**.

## LabelWrapperValidClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<label>` **when the model member is in a valid state**.

## LabelWrapperInvalidClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<label>` **when the model member is in an invalid state**.

## LabelValidClasses
Type: `string`

This specify any CSS classes (space-separated) that should be applied to the `<label>` **when the model member is in a valid state**.

## LabelInvalidClasses
Type: `string`

This specify any CSS classes (space-separated) that should be applied to the `<label>` **when the model member is in an invalid state**.

## InputWrapperValidClasses
Type: `string`

This specify any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<input>` **when the model member is in a valid state**.

## InputWrapperInvalidClasses
Type: `string`

This specify any CSS classes (space-separated) that should be applied to the `<div>` that surrounds the `<input>` **when the model member is in an invalid state**.

## InputValidClasses
Type: `string`

This specify any CSS classes (space-separated) that should be applied to the `<input>` **when the model member is in a valid state**.

## InputInvalidClasses
Type: `string`

This specify any CSS classes (space-separated) that should be applied to the `<input>` **when the model member is in an invalid state**.

## ErrorWrapperClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<ul>` that contains validation error messages. Because this block is only intended to display error information, there are no validation state-specific classes.

## ErrorClasses
Type: `string`

This property specifies any CSS classes (space-separated) that should be applied to the `<li>`s that contain individual validation error messages. Because this element is only intended to display error information, there are no validation state-specific classes.

## AlwaysRenderErrorContainer
Type: `bool`

This property specifies whether the `<ul>` that contains validation error messages should always be rendered. If `true`, the `<ul>` will be rendered even if there are no validation messages. This can be helpful if you intend to perform client-side form validation; by ensuring the `<ul>` is always rendered, your JavaScript can skip creating the error message block and jump straight to populating it with errors.