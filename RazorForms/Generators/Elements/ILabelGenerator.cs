using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Elements;

public interface ILabelGenerator : IValidityAwareOutputGenerator<IFormComponentWithValidationOptions>
{
}