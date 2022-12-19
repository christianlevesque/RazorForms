namespace RazorForms.Options;

public class FormComponentWithValidationOptions : FormComponentOptions
{
	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component when model validation succeeds
	/// </summary>
	public string ComponentWrapperValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component when model validation fails
	/// </summary>
	public string ComponentWrapperErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block when model validation succeeds
	/// </summary>
	public string InputBlockWrapperValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block when model validation fails
	/// </summary>
	public string InputBlockWrapperErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt; when model validation succeeds
	/// </summary>
	public string LabelWrapperValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt; when model validation fails
	/// </summary>
	public string LabelWrapperErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;label&gt; when model validation succeeds
	/// </summary>
	public string LabelValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;label&gt; when model validation fails
	/// </summary>
	public string LabelErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt; when model validation succeeds
	/// </summary>
	public string InputWrapperValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt; when model validation fails
	/// </summary>
	public string InputWrapperErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;input&gt; when model validation succeeds
	/// </summary>
	public string InputValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;input&gt; when model validation fails
	/// </summary>
	public string InputErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;ul&gt; containing the input validation errors
	/// </summary>
	public string ErrorWrapperClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;li&gt; containing each input validation error
	/// </summary>
	public string ErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// Whether to always show the error container element, regardless of validation state
	/// </summary>
	/// <remarks>
	/// Setting this value to <c>true</c> will simplify frontend input validation because the error container will exist regardless of validation state.
	/// </remarks>
	public bool AlwaysShowErrorContainer { get; set; }
}