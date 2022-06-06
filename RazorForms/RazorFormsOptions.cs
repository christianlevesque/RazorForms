namespace RazorForms;

public class RazorFormsOptions
{
	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component
	/// </summary>
	public string ComponentWrapperClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component when model validation succeeds
	/// </summary>
	public string ComponentWrapperValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component when model validation fails
	/// </summary>
	public string ComponentWrapperErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block
	/// </summary>
	public string InputBlockWrapperClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block when model validation succeeds
	/// </summary>
	public string InputBlockWrapperValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block when model validation fails
	/// </summary>
	public string InputBlockWrapperErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt;
	/// </summary>
	public string LabelWrapperClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt; when model validation succeeds
	/// </summary>
	public string LabelWrapperValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt; when model validation fails
	/// </summary>
	public string LabelWrapperErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;label&gt;
	/// </summary>
	public string LabelClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;label&gt; when model validation succeeds
	/// </summary>
	public string LabelValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;label&gt; when model validation fails
	/// </summary>
	public string LabelErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt;
	/// </summary>
	public string InputWrapperClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt; when model validation succeeds
	/// </summary>
	public string InputWrapperValidClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt; when model validation fails
	/// </summary>
	public string InputWrapperErrorClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;input&gt;
	/// </summary>
	public string InputClasses { get; set; } = string.Empty;

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
}