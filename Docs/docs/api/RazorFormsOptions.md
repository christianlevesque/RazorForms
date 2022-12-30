# RazorFormsOptions
Extends: (none)

The `RazorFormsOptions` class is used to store the entire RazorForms configuration. This class is injected into RazorForms as a singleton into your service collection when you call `UseRazorForms()` or a library-specific extension method such as `UseRazorFormsWithBulma()`.

`RazorFormsOptions` contains a single property for each tag helper in RazorForms. Each of these properties contains the configuration options for its respective tag helper.

## CheckInputOptions
Type: [RazorForms.Options.FormComponentOptions](/docs/api/FormComponentOptions)

This property contains the configuration options for the [<check-input> tag helper](/docs/tag-helpers/check-input).

## RadioInputOptions
Type: [RazorForms.Options.FormComponentOptions](/docs/api/FormComponentOptions)

This property contains the configuration options for the [<radio-input> tag helper](/docs/tag-helpers/radio-input).

## TextInputOptions
Type: [RazorForms.Options.ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions)

This property contains the configuration options for the [<text-input> tag helper](/docs/tag-helpers/text-input).

## TextAreaInputOptions
Type: [RazorForms.Options.ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions)

This property contains the configuration options for the [<text-area-input> tag helper](/docs/tag-helpers/text-area-input).

## SelectInputOptions
Type: [RazorForms.Options.ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions)

This property contains the configuration options for the [<select-input> tag helper](/docs/tag-helpers/select-input).

## CheckInputGroupOptions
Type: [RazorForms.Options.ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions)

This property contains the configuration options for the [<check-input-group> tag helper](/docs/tag-helpers/check-input-group).

## RadioInputGroupOptions
Type: [RazorForms.Options.ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions)

This property contains the configuration options for the [<radio-input-group> tag helper](/docs/tag-helpers/radio-input-group).