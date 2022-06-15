using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Elements;

public class InputBlockWrapperGenerator : ValidityAwareOutputGenerator<IFormComponentOptions>, IInputBlockWrapperGenerator
{
	/// <inheritdoc />
	public override Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                            RazorFormsTagHelperBase helper,
	                                            TagHelperAttributeList? attributes = null,
	                                            TagHelperContent? childContent = null)
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

		if (Options is IFormComponentWithValidationOptions withValidationOptions)
		{
			ApplyClasses(output, 
			             withValidationOptions.InputBlockWrapperClasses, 
			             withValidationOptions.InputBlockWrapperValidClasses, 
			             withValidationOptions.InputBlockWrapperErrorClasses);
			
		}
		else
		{
			ApplyClasses(output, Options.InputBlockWrapperClasses);
		}
	}
}