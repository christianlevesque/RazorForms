using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms;

public class FormInput : IAdditionalFormClasses
{
	public IHtmlContent? ChildContent { get; set; }
	public IList<string> Errors { get; set; } = new List<string>();
	public string DisplayName { get; set; } = default!;
	public string MvcName { get; set; } = default!;
	public object? Value { get; set; }
	public bool IsValid { get; set; }
	public bool RemoveWrappers { get; set; }
	public bool InputFirst { get; set; }
	public TagHelperAttributeList Attributes { get; set; } = default!;
	public InputType Type { get; set; }
	public string AdditionalComponentWrapperClasses { get; set; } = string.Empty;
	public string AdditionalInputBlockWrapperClasses { get; set; } = string.Empty;
	public string AdditionalLabelWrapperClasses { get; set; } = string.Empty;
	public string AdditionalLabelClasses { get; set; } = string.Empty;
	public string AdditionalInputWrapperClasses { get; set; } = string.Empty;
	public string AdditionalErrorWrapperClasses { get; set; } = string.Empty;
	public string AdditionalErrorClasses { get; set; } = string.Empty;
}