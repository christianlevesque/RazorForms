# ValidityAwareMarkupModel
Extends: [RazorForms.Models.MarkupModel<ValidityAwareFormComponentOptions>](/docs/api/MarkupModel)

The `ValidityAwareMarkupModel` class is the model used for rendering validity-aware tag helper content to a template file.

## LabelHtml
Type: `System.Collections.Generic.IEnumerable<string>`

This property represents the field validation errors for the model member represented by the current tag helper.

## IsValid
Type: `bool`

This property represents whether the model member explicitly passes validation.

## IsInvalid
Type: `bool`

This property represents whether the model member explicitly fails validation.