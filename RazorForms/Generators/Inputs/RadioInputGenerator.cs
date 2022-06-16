namespace RazorForms.Generators.Inputs;

public class RadioInputGenerator : CheckRadioInputGeneratorBase, IRadioInputGenerator
{
	/// <inheritdoc />
	public RadioInputGenerator()
	{
		TypeAttributeValue = "radio";
	}
}