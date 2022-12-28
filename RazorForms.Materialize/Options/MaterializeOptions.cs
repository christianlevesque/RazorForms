using RazorForms.Options;

namespace RazorForms.Materialize.Options;

public class MaterializeOptions : RazorFormsOptions
{
	/// <summary>
	/// Represents the configuration options for the &lt;date-picker-input&gt; tag helper
	/// </summary>
	public FormattableOptions DatePickerInputOptions { get; set; } = new();
}