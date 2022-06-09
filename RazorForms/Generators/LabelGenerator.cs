using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public class LabelGenerator : OutputGeneratorBase
{
	protected IFormComponentOptions Options;

	/// <inheritdoc />
	public LabelGenerator(IFormComponentOptions options, bool isValid, bool isInvalid) : base(isValid, isInvalid)
	{
		Options = options;
	}

	/// <inheritdoc />
	public override async Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     TextInputTagHelper helper,
	                                                     TagHelperAttributeList? attributes = null)
	{
		var labelHelper = new LabelTagHelper(helper.Generator)
		{
			ViewContext = helper.ViewContext,
			For = helper.For
		};

		var labelOutput = new TagHelperOutput(tagName: "label",
		                                      attributes: new TagHelperAttributeList(),
		                                      getChildContentAsync: DefaultTagHelperContent);

		ApplyBaseClasses(labelOutput);

		await labelHelper.ProcessAsync(context, labelOutput);

		return Options.RemoveWrappers ?? false
			       ? labelOutput
			       : GenerateWrapper(labelOutput.Content);
	}

	/// <inheritdoc />
	protected virtual void ApplyBaseClasses(TagHelperOutput output)
	{
		ApplyClasses(output,
		             Options.LabelClasses,
		             Options.LabelValidClasses,
		             Options.LabelErrorClasses);
	}

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		ApplyClasses(output,
		             Options.LabelWrapperClasses,
		             Options.LabelWrapperValidClasses,
		             Options.LabelWrapperErrorClasses);
	}
}