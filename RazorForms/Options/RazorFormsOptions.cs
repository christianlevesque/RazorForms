namespace RazorForms.Options;

public class RazorFormsOptions
{
	/// <summary>
	/// Represents the configuration options for the &lt;text-input&gt; tag helper
	/// </summary>
	public FormComponentWithValidationOptions TextInputOptions { get; set; } = new();

	/// <summary>
	/// Represents the configuration options for the &lt;check-input&gt; tag helper
	/// </summary>
	public FormComponentOptions CheckInputOptions { get; set; } = new();

	/// <summary>
	/// Represents the configuration options for the &lt;radio-input&gt; tag helper
	/// </summary>
	public FormComponentOptions RadioInputOptions { get; set; } = new();

	/// <summary>
	/// Represents the configuration options for the &lt;check-input-group&gt; tag helper
	/// </summary>
	public FormComponentWithValidationOptions CheckInputGroupOptions { get; set; } = new();

	/// <summary>
	/// Represents the configuration options for the &lt;radio-input-group&gt; tag helper
	/// </summary>
	public FormComponentWithValidationOptions RadioInputGroupOptions { get; set; } = new();

	/// <summary>
	/// Represents the configuration options for the &lt;text-area-input&gt; tag helper
	/// </summary>
	public FormComponentWithValidationOptions TextAreaInputOptions { get; set; } = new();

	/// <summary>
	/// Represents the configuration options for the &lt;select-input&gt; tag helper
	/// </summary>
	public FormComponentWithValidationOptions SelectInputOptions { get; set; } = new();
}