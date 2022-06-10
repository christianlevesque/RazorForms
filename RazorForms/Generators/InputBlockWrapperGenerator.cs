using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public class InputBlockWrapperGenerator : OutputGeneratorBase<IFormComponentOptions>, IInputBlockWrapperGenerator
{
	/// <inheritdoc />
	public override Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     RazorFormsTagHelperBase helper,
	                                                     TagHelperAttributeList? attributes = null)
	{
		ThrowIfNotInitialized();

		attributes ??= new TagHelperAttributeList();
		var output = new TagHelperOutput("div",
		                                 attributes,
		                                 DefaultTagHelperContent);

		ApplyWrapperClasses(output);
		return Task.FromResult(output);
	}

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output,
		             Options.InputBlockWrapperClasses,
		             Options.InputBlockWrapperValidClasses,
		             Options.InputBlockWrapperErrorClasses);
	}
}