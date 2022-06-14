using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.Generators.Buttons;

public class ResetButtonGenerator : ButtonGeneratorBase, IResetButtonGenerator
{
	/// <inheritdoc />
	protected override void ApplyBaseClasses(TagHelperOutput output)
	{
		ApplyClasses(output, Options.ResetButtonClasses);
	}
}