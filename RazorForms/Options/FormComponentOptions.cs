namespace RazorForms.Options;

/// <summary>
/// The options used to configure validity-unaware tag helpers
/// </summary>
public class FormComponentOptions
{
	/// <summary>
	/// Specifies the root-relative path to the tag helper's Razor template
	/// </summary>
	public string TemplatePath { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component
	/// </summary>
	public string ComponentWrapperClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt;
	/// </summary>
	public string LabelWrapperClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;label&gt;
	/// </summary>
	public string LabelClasses { get; set; } = string.Empty;

	/// <summary>
	/// Which HTML tag, if any, to wrap the &lt;label&gt; text with
	/// </summary>
	public string LabelTextHtmlWrapper { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt;
	/// </summary>
	public string InputWrapperClasses { get; set; } = string.Empty;

	/// <summary>
	/// CSS classes applied to the &lt;input&gt;
	/// </summary>
	public string InputClasses { get; set; } = string.Empty;

	/// <summary>
	/// Determines whether or not to remove the &lt;div&gt; surrounding &lt;label&gt; and &lt;input&gt; elements
	/// </summary>
	public bool RemoveWrappers { get; set; }

	/// <summary>
	/// Determines whether the &lt;input&gt; should come first in the markup or not
	/// </summary>
	public bool InputFirst { get; set; }

	/// <summary>
	/// Determines whether the &lt;input&gt; should be rendered inside its associated &lt;label&gt;
	/// </summary>
	public bool RenderInputInsideLabel { get; set; }
}