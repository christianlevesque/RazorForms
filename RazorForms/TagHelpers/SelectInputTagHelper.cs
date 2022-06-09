using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public class SelectInputTagHelper : InputHelperBase<ISelectOptions>
{
	/// <inheritdoc />
	public SelectInputTagHelper(IHtmlHelper html, ISelectOptions options) : base(html, options)
	{
	}

	/// <inheritdoc />
	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		await base.ProcessAsync(context, output);
		var model = await GetComponentModel<FormInput<IFormComponentOptions>>(output);
		model.Type = InputType.Select;

		var content = await Html.PartialAsync("~/Templates/TextInput.cshtml", model);
		output.Content.SetHtmlContent(content);
	}
}