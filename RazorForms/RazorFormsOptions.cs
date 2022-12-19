using RazorForms.Options;

namespace RazorForms;

public class RazorFormsOptions
{
	public FormComponentWithValidationOptions InputOptions { get; set; } = new();
	public FormComponentOptions CheckInputOptions { get; set; } = new();
	public FormComponentOptions RadioInputOptions { get; set; } = new();
	public FormComponentWithValidationOptions CheckInputGroupOptions { get; set; } = new();
	public FormComponentWithValidationOptions RadioInputGroupOptions { get; set; } = new();
	public FormComponentWithValidationOptions TextAreaOptions { get; set; } = new();
	public FormComponentWithValidationOptions SelectOptions { get; set; } = new();
}