using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers.Elements;

public class RadioInputGroupTagHelper : ValidityAwareTagHelperBase
{
	public RadioInputGroupTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		RazorFormsOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options.CheckInputGroupOptions)
	{
		LabelReceivesChildContent = false;
		InputTag = string.Empty;
		InputTagMode = TagMode.StartTagAndEndTag;
	}
}