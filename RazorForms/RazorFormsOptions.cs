using RazorForms.Options;

namespace RazorForms;

public class RazorFormsOptions
{
	public IInputOptions InputOptions { get; set; } = new InputOptions();
	public ISelectOptions SelectOptions { get; set; } = new SelectOptions();
	public IButtonOptions ButtonOptions { get; set; } = new ButtonOptions();
}