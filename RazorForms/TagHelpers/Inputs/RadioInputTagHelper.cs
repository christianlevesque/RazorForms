using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers.Inputs;

/// <summary>
/// Creates a radio &lt;input&gt;
/// </summary>
public class RadioInputTagHelper : CheckRadioTagHelperBase
{
	/// <inheritdoc />
	public RadioInputTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		RazorFormsOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options.CheckInputOptions)
	{
		Type = "radio";
	}

	/// <inheritdoc />
	protected override void AddCheckedAttribute(TagHelperAttributeList attributes)
	{
		var currentValue = attributes.FirstOrDefault(a => a.Name == "value");
		if (currentValue == null)
		{
			return;
		}

		var setValue = ViewContext.ViewData.Eval(For!.Name);
		if (setValue == null)
		{
			return;
		}

		if (currentValue.Value.ToString() == setValue.ToString())
		{
			attributes.Add(HtmlCheckedAttributeName, null);
		}
	}
}