using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.TagHelpers.Inputs;

/// <summary>
/// Creates a &lt;textarea&gt; input
/// </summary>
public class TextAreaInputTagHelper : ValidityAwareTagHelperBase<ValidityAwareFormComponentOptions>
{
	public TextAreaInputTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		RazorFormsOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options.TextAreaInputOptions)
	{
		InputTag = "textarea";
		InputTagMode = TagMode.StartTagAndEndTag;
		LabelReceivesChildContent = true;
	}

	/// <inheritdoc />
	protected override TagHelper CreateInputTagHelper()
	{
		return new TextAreaTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For
		};
	}
}