using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorForms.Materialize.Options;

namespace RazorForms.Materialize.TagHelpers;

public class TimePickerInputTagHelper : DateTimePickerBase
{
	public TimePickerInputTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		MaterializeOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options.TimePickerInputOptions)
	{
	}
}