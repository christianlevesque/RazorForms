using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Materialize.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Materialize.TagHelpers;

public abstract class DateTimePickerBase : ValidityAwareTagHelperBase<FormattableOptions>
{
	[HtmlAttributeName("asp-format")]
	public string? Format { get; set; }

	public DateTimePickerBase(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		FormattableOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options)
	{
		LabelReceivesChildContent = true;
	}

	protected override TagHelper CreateInputTagHelper()
	{
		return new InputTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For,
			Format = Format ?? Options.Format
		};
	}

	protected override void AddCustomInputAttributes(TagHelperAttributeList attributes)
	{
		attributes.Add("type", "text");
	}
}