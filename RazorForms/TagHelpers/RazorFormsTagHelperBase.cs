using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.TagHelpers;

public class RazorFormsTagHelperBase : TagHelper
{
	protected const string ForAttributeName = "asp-for";

	public readonly IHtmlGenerator Generator;

    [HtmlAttributeNotBound]
    [ViewContext]
	public ViewContext? ViewContext { get; set; }

	[HtmlAttributeName(ForAttributeName)]
	public ModelExpression? For { get; set; }

	/// <inheritdoc />
	protected RazorFormsTagHelperBase(IHtmlGenerator generator)
	{
		Generator = generator;
	}
}