using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.TagHelpers;

namespace RazorForms.Materialize.TagHelpers;

public class DatePickerInputTagHelper : ValidityAwareTagHelperBase
{
	[HtmlAttributeName("asp-format")]
	public string? Format { get; set; }

	public DatePickerInputTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		MaterializeOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options.DatePickerInputOptions)
	{
		LabelReceivesChildContent = true;
	}

	protected override TagHelper CreateInputTagHelper()
	{
		return new InputTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For,
			Format = Format ?? "{0:MMM dd, yyyy}" // Same format used by Materialize
		};
	}

	protected override void AddCustomInputAttributes(TagHelperAttributeList attributes)
	{
		attributes.Add("type", "text");
	}
}