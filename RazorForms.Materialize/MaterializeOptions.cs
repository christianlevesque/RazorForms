using RazorForms.Options;

namespace RazorForms.Materialize;

public class MaterializeOptions : RazorFormsOptions
{
	/// <summary>
	/// Represents the configuration options for the &lt;date-picker-input&gt; tag helper
	/// </summary>
	public ValidityAwareFormComponentOptions DatePickerInputOptions { get; set; } = new();
}