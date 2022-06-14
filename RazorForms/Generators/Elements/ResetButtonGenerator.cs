using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.Generators.Elements;

public class ResetButtonGenerator : ButtonGeneratorBase, IResetButtonGenerator
{
	/// <inheritdoc />
	protected override void ApplyBaseClasses(TagHelperOutput output)
	{
		ApplyClasses(output, Options.ResetButtonClasses);
	}
}