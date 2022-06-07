using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.TagHelpers;

public class TagHelperBase : TagHelper
{
	protected readonly IHtmlHelper Html;

	/// <inheritdoc />
	public TagHelperBase(IHtmlHelper html)
	{
		Html = html;
	}

	[HtmlAttributeNotBound]
	[ViewContext]
	public ViewContext ViewContext { get; set; } = default!;

	/// <inheritdoc />
	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		(Html as IViewContextAware)!.Contextualize(ViewContext);
		output.TagName = string.Empty;
		return Task.CompletedTask;
	}
}