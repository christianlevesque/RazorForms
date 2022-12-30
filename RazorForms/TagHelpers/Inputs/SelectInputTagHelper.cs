using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers.Inputs;

/// <summary>
/// Creates a &lt;select&gt; input
/// </summary>
public class SelectInputTagHelper : ValidityAwareTagHelperBase<ValidityAwareFormComponentOptions>
{
	protected const string ItemsAttributeName = "asp-items";

	[HtmlAttributeName(ItemsAttributeName)]
	public IEnumerable<SelectListItem>? Items { get; set; }

	/// <inheritdoc/>
	public SelectInputTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		RazorFormsOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options.SelectInputOptions)
	{
		InputTag = "select";
		InputTagMode = TagMode.StartTagAndEndTag;
	}

	protected override TagHelper CreateInputTagHelper()
	{
		return new SelectTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For,
			Items = Items
		};
	}
}