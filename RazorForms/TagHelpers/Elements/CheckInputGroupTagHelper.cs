using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.TagHelpers.Elements;

public class CheckInputGroupTagHelper : ValidityAwareTagHelperBase
{
	public CheckInputGroupTagHelper(
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