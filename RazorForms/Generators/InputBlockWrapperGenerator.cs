using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public class InputBlockWrapperGenerator : OutputGeneratorBase
{
	protected IFormComponentOptions Options;

	/// <inheritdoc />
	public InputBlockWrapperGenerator(IFormComponentOptions options, bool isValid, bool isInvalid) : base(isValid, isInvalid)
	{
		Options = options;
	}

	/// <inheritdoc />
	public override Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     TextInputTagHelper helper,
	                                                     TagHelperAttributeList? attributes = null)
	{
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
		ApplyClasses(output,
		             Options.InputBlockWrapperClasses,
		             Options.InputBlockWrapperValidClasses,
		             Options.InputBlockWrapperErrorClasses);
	}
}