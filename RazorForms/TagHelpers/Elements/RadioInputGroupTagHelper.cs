using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers.Elements;

/// <summary>
/// Wraps around a series of &lt;radio-input&gt; tag helpers to create a group label and error output
/// </summary>
public class RadioInputGroupTagHelper : GroupTagHelperBase
{
	public RadioInputGroupTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		RazorFormsOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options.RadioInputGroupOptions)
	{
	}
}