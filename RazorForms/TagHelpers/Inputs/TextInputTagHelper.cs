using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers.Inputs;

/// <summary>
/// Creates a text- or numeric-based &lt;input&gt;
/// </summary>
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
			options.TextInputOptions)
	{
		LabelReceivesChildContent = true;
	}

	/// <inheritdoc />
	protected override TagHelper CreateInput(TagHelperAttributeList attributes)
	{
		return new InputTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For,
			Format = Format
		};
	}
}