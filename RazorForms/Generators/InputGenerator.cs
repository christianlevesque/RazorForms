using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public class InputGenerator : OutputGeneratorBase
{
	protected IFormComponentOptions Options;
	/// <inheritdoc />
	public InputGenerator(IFormComponentOptions options, bool isValid, bool isInvalid) : base(isValid, isInvalid)
	{
		Options = options;
	}

	public override Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     TextInputTagHelper helper,
	                                                     TagHelperAttributeList? attributes = null)
	{
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
		ApplyClasses(output,
		             Options.InputClasses,
		             Options.InputValidClasses,
		             Options.InputErrorClasses);
	}

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		ApplyClasses(output,
		             Options.InputWrapperClasses,
		             Options.InputWrapperValidClasses,
		             Options.InputWrapperErrorClasses);
	}
}