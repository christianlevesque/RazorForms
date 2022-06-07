using RazorForms.Options;

namespace RazorForms;

public class RazorFormsOptions
{
	public IInputOptions InputOptions { get; set; } = new InputOptions();
}