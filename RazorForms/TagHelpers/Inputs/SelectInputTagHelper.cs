using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using RazorForms.Options;

namespace RazorForms.TagHelpers.Inputs;

public class SelectInputTagHelper : ValidityAwareTagHelperBase
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
		LabelReceivesChildContent = false;
	}

	protected override TagHelper CreateInput(TagHelperAttributeList attributes)
	{
		return new SelectTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For,
			Items = Items
		};
	}
}