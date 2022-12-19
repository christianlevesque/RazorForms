﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

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
		InputTag = "div";
		InputTagMode = TagMode.StartTagAndEndTag;
	}
}