using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public class InputGenerator : OutputGeneratorBase<IFormComponentOptions>, IInputGenerator
{

	public override Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     RazorFormsTagHelperBase helper,
	                                                     TagHelperAttributeList? attributes = null)
	{
		ThrowIfNotInitialized();

		attributes ??= new TagHelperAttributeList();

		var inputHelper = new InputTagHelper(helper.Generator)
		{
			ViewContext = helper.ViewContext,
			For = helper.For,
			Format = helper.Format
		};

		var output = new TagHelperOutput(tagName: "input",
		                                 attributes: new TagHelperAttributeList(attributes),
		                                 getChildContentAsync: DefaultTagHelperContent)
		{
			TagMode = TagMode.SelfClosing
		};

		ApplyBaseClasses(output);

		inputHelper.Process(context, output);

		var result = Options.RemoveWrappers ?? false
			             ? output
			             : GenerateWrapper(output.Content);

		return Task.FromResult(result);
	}

	protected virtual void ApplyBaseClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output,
		             Options.InputClasses,
		             Options.InputValidClasses,
		             Options.InputErrorClasses);
	}

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output,
		             Options.InputWrapperClasses,
		             Options.InputWrapperValidClasses,
		             Options.InputWrapperErrorClasses);
	}
}