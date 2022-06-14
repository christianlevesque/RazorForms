using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Generators.Elements;
using RazorForms.Options.Elements;

namespace RazorForms.TagHelpers.Elements;

public abstract class ButtonHelperBase : TagHelper
{
	protected IButtonGenerator Generator;
	protected IButtonOptions Options;
	protected ButtonType Type;

	protected ButtonHelperBase(IButtonGenerator generator,
	                           IButtonOptions options,
	                           ButtonType type)
	{
		Generator = generator;
		Options = options;
		Type = type;
	}

	/// <inheritdoc />
	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		output.TagName = "";

		Generator.Init(Options, Type);
		var htmlOutput = await Generator.GenerateOutput(context,
		                                                output.Attributes,
		                                                await output.GetChildContentAsync());
		output.Content.SetHtmlContent(Generator.Render(htmlOutput));
	}
}