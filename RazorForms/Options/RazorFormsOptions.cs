namespace RazorForms.Options;

/// <summary>
/// Contains the full group of configuration options for each tag helper used by RazorForms
/// </summary>
public class RazorFormsOptions
{
	/// <summary>
	/// Represents the configuration options for the &lt;text-input&gt; tag helper
	/// </summary>
	public ValidityAwareFormComponentOptions TextInputOptions { get; set; } = new();

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
	public ValidityAwareFormComponentOptions CheckInputGroupOptions { get; set; } = new();

	/// <summary>
	/// Represents the configuration options for the &lt;radio-input-group&gt; tag helper
	/// </summary>
	public ValidityAwareFormComponentOptions RadioInputGroupOptions { get; set; } = new();

	/// <summary>
	/// Represents the configuration options for the &lt;text-area-input&gt; tag helper
	/// </summary>
	public ValidityAwareFormComponentOptions TextAreaInputOptions { get; set; } = new();

	/// <summary>
	/// Represents the configuration options for the &lt;select-input&gt; tag helper
	/// </summary>
	public ValidityAwareFormComponentOptions SelectInputOptions { get; set; } = new();
}