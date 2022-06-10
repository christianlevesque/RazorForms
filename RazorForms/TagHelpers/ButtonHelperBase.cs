using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing.Matching;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public abstract class ButtonHelperBase : TagHelperBaseLegacy
{
	protected ButtonType Type;
	protected IButtonOptions Options;

	/// <inheritdoc />
	protected ButtonHelperBase(IHtmlHelper html, IButtonOptions options) : base(html)
	{
		Options = options;
	}

	/// <inheritdoc />
	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		await base.ProcessAsync(context, output);
		var model = new FormButton
		{
			ChildContent = await output.GetChildContentAsync(),
			Attributes = output.Attributes,
			Type = Type,
			Options = Options
		};

		var content = await Html.PartialAsync("~/Templates/Elements/Button.cshtml", model);
		output.Content.SetHtmlContent(content);
	}
}