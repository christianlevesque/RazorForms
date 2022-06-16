using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Inputs;

public interface IInputGenerator : IValidityAwareOutputGenerator<IFormComponentWithValidationOptions>
{
}