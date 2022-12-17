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
	}

	protected override Task<TagHelperOutput> CreateInput(
		TagHelperContext context,
		TagHelperOutput output,
		TagHelperAttributeList attributes)
	{
		var inputHelper = new InputTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For,
			Format = Format
		};

		var inputOutput = new TagHelperOutput(
			"input",
			attributes,
			DefaultTagHelperContent)
		{
			TagMode = TagMode.SelfClosing
		};

		ApplyCssClassesToInput(inputOutput);

		inputHelper.Process(context, inputOutput);
		return Task.FromResult(inputOutput);
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