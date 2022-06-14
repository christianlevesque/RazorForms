using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.Generators.Elements;

public class SubmitButtonGenerator : ButtonGeneratorBase, ISubmitButtonGenerator
{
	/// <inheritdoc />
	protected override void ApplyBaseClasses(TagHelperOutput output)
	{
		ApplyClasses(output, Options.SubmitButtonClasses);
	}
}