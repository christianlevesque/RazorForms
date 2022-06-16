using RazorForms.Options;

namespace RazorForms.Generators.Inputs;

public interface IInputGenerator : IValidityAwareOutputGenerator<IFormComponentWithValidationOptions>
{
}