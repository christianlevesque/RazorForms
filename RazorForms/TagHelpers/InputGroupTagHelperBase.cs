using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

/// <summary>
/// Adds common functionality for use among all validity-aware tag helpers that wrap around validity-unaware tag helpers
/// </summary>
public abstract class GroupTagHelperBase : ValidityAwareTagHelperBase
{
	public GroupTagHelperBase(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		ValidityAwareFormComponentOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options)
	{
		InputTag = string.Empty;
		InputTagMode = TagMode.StartTagAndEndTag;
	}
}