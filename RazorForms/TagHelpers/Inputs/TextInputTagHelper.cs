using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers.Inputs;

public class TextInputTagHelper : ValidityAwareTagHelperBase
{
	protected const string FormatAttributeName = "asp-format";

	[HtmlAttributeName(FormatAttributeName)]
	public string? Format { get; set; }

	public TextInputTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		RazorFormsOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options.InputOptions)
	{
		LabelReceivesChildContent = true;
		InputTag = "input";
		InputTagProcessingIsAsync = false;
	}

	protected override TagHelper CreateInput(TagHelperAttributeList attributes)
	{
		return new InputTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For,
			Format = Format
		};
	}

	protected override void ApplyCssClassesToInput(TagHelperOutput input)
	{
		AddValidityAwareClasses(
			input,
			Options.InputClasses,
			Options.InputValidClasses,
			Options.InputErrorClasses);
	}

	protected override void ApplyCssClassesToLabel(TagHelperOutput label)
	{
		AddValidityAwareClasses(
			label,
			Options.LabelClasses,
			Options.LabelValidClasses,
			Options.LabelErrorClasses);
	}
}