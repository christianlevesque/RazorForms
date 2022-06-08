using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms;

public class FormElementBase<TOptions>
{
	/// <summary>
	/// The child content of the HTML element
	/// </summary>
	public TagHelperContent ChildContent { get; set; } = default!;

	/// <summary>
	/// The list of HTML attributes passed to the form element component
	/// </summary>
	public TagHelperAttributeList Attributes { get; set; } = default!;

	/// <summary>
	/// The options to pass to the component for use during rendering
	/// </summary>
	public TOptions Options { get; set; } = default!;
}