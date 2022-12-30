using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.Models;

/// <summary>
/// The model used when rendering validity-unaware content 
/// </summary>
public class MarkupModel
{
	/// <summary>
	/// Represents the HTML content of the &lt;input&gt; element
	/// </summary>
	/// <remarks>
	/// The input HTML will be null if the <see cref="FormComponentOptions.RenderInputInsideLabel"/> option is <c>true</c>.
	/// </remarks>
	public TagHelperOutput? InputHtml { get; set; }

	/// <summary>
	/// Represents the HTML content of the &lt;label&gt; element
	/// </summary>
	public TagHelperOutput LabelHtml { get; set; } = default!;

	/// <summary>
	/// Configuration options for this form element type
	/// </summary>
	public object ElementOptions { get; set; } = default!;
}