using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms;

public class FormInput<TOptions> : IFormInput<TOptions> where TOptions : IFormComponentOptions
{
	/// <summary>
	/// The child content of a form input
	/// </summary>
	/// <remarks>
	/// Not all inputs use child content. 
	/// </remarks>
	public TagHelperContent ChildContent { get; set; } = default!;

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
	/// Determines whether or not the &lt;label&gt; should receive the <see cref="ChildContent"/> value 
	/// </summary>
	/// <remarks>
	/// If <c>true</c>, the <see cref="ChildContent"/> should be used as the inner HTML for the &lt;label&gt; element. Otherwise, it should be reserved for use elsewhere in the component.
	/// </remarks>
	public bool LabelAcceptsChildContent { get; set; }

	/// <summary>
	/// The options to pass to the component for use during rendering
	/// </summary>
	public TOptions Options { get; set; } = default!;
}