using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.Models;

/// <summary>
/// The model used when rendering validity-unaware content 
/// </summary>
/// <typeparam name="TOptions">The type of the options object</typeparam>
public class MarkupModel<TOptions>
	where TOptions : FormComponentOptions
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
	public TOptions ElementOptions { get; set; } = default!;
}