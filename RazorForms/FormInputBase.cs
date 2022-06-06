using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms;

public class FormInputBase
{
	public IHtmlContent? ChildContent { get; set; }
	public IList<string> Errors { get; set; } = new List<string>();
	public string DisplayName { get; set; } = default!;
	public string MvcName { get; set; } = default!;
	public object? Value { get; set; }
	public bool IsValid { get; set; }
	public TagHelperAttributeList Attributes { get; set; } = default!;
}