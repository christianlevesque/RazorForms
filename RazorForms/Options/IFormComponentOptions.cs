namespace RazorForms.Options;

public interface IFormComponentOptions : IComponentOptions
{
	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block
	/// </summary>
	string? InputBlockWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt;
	/// </summary>
	string? LabelWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;label&gt;
	/// </summary>
	string? LabelClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt;
	/// </summary>
	string? InputWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;input&gt;
	/// </summary>
	string? InputClasses { get; set; }

	/// <summary>
	/// Determines whether the &lt;input&gt; should come first in the markup or not
	/// </summary>
	bool? InputFirst { get; set; }

	/// <summary>
	/// Determines whether the &lt;input&gt; should be rendered inside its associated &lt;label&gt;
	/// </summary>
	bool? RenderInputInsideLabel { get; set; }
}