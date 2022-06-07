using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms;

public class FormInput
{
	/// <summary>
	/// The child content of a form input
	/// </summary>
	/// <remarks>
	/// Not all inputs use child content. 
	/// </remarks>
	public IHtmlContent? ChildContent { get; set; }

	/// <summary>
	/// A list of error messages from the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>
	/// </summary>
	public IList<string> Errors { get; set; } = new List<string>();

	/// <summary>
	/// The human-friendly string to use in the &lt;label&gt;
	/// </summary>
	public string DisplayName { get; set; } = default!;

	/// <summary>
	/// The name of the form element as expected by the framework
	/// </summary>
	public string MvcName { get; set; } = default!;

	/// <summary>
	/// The existing value of the form element, if any
	/// </summary>
	public object? Value { get; set; }

	/// <summary>
	/// Whether the form element explicitly passes validation (i.e., returns <c>false</c> if validation has not been run)
	/// </summary>
	public bool IsValid { get; set; }

	/// <summary>
	/// The list of HTML attributes passed to the form element component
	/// </summary>
	public TagHelperAttributeList Attributes { get; set; } = default!;

	/// <summary>
	/// The type of the form element
	/// </summary>
	public InputType Type { get; set; }

	/// <summary>
	/// The <see cref="RazorFormsOptions"/> to pass to the component for use during rendering
	/// </summary>
	public RazorFormsOptions Options { get; set; } = default!;
}