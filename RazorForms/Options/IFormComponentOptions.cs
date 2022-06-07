namespace RazorForms.Options;

public interface IFormComponentOptions : IComponentWithValidationOptions
{
	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block
	/// </summary>
	string? InputBlockWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block when model validation succeeds
	/// </summary>
	string? InputBlockWrapperValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block when model validation fails
	/// </summary>
	string? InputBlockWrapperErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt;
	/// </summary>
	string? LabelWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt; when model validation succeeds
	/// </summary>
	string? LabelWrapperValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt; when model validation fails
	/// </summary>
	string? LabelWrapperErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;label&gt;
	/// </summary>
	string? LabelClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;label&gt; when model validation succeeds
	/// </summary>
	string? LabelValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;label&gt; when model validation fails
	/// </summary>
	string? LabelErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt;
	/// </summary>
	string? InputWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt; when model validation succeeds
	/// </summary>
	string? InputWrapperValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt; when model validation fails
	/// </summary>
	string? InputWrapperErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;input&gt;
	/// </summary>
	string? InputClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;input&gt; when model validation succeeds
	/// </summary>
	string? InputValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;input&gt; when model validation fails
	/// </summary>
	string? InputErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;ul&gt; containing the input validation errors
	/// </summary>
	string? ErrorWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;li&gt; containing each input validation error
	/// </summary>
	string? ErrorClasses { get; set; }

	/// <summary>
	/// Determines whether the &lt;input&gt; should come first in the markup or not
	/// </summary>
	bool? InputFirst { get; set; }
}