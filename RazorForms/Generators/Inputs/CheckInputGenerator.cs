namespace RazorForms.Generators.Inputs;

public class CheckInputGenerator : CheckRadioInputGeneratorBase, ICheckInputGenerator
{
	/// <inheritdoc />
	public CheckInputGenerator()
	{
		TypeAttributeValue = "checkbox";
	}
}