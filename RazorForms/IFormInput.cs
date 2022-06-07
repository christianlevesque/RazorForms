using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms;

public interface IFormInput<TOptions> where TOptions : IFormComponentOptions
{
	IHtmlContent? ChildContent { get; set; }
	IList<string> Errors { get; set; }
	string DisplayName { get; set; }
	string MvcName { get; set; }
	object? Value { get; set; }
	bool IsValid { get; set; }
	TagHelperAttributeList Attributes { get; set; }
	InputType Type { get; set; }
	TOptions Options { get; set; }
}