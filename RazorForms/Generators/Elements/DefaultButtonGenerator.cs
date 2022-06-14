using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.Generators.Elements;

public class DefaultButtonGenerator : ButtonGeneratorBase, IDefaultButtonGenerator
{
	/// <inheritdoc />
	protected override void ApplyBaseClasses(TagHelperOutput output)
	{
		ApplyClasses(output, Options.DefaultButtonClasses);
	}
}