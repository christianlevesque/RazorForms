using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public class LabelGenerator : OutputGeneratorBase<IFormComponentOptions>, ILabelGenerator
{
	/// <inheritdoc />
	public override async Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                           RazorFormsTagHelperBase helper,
	                                                           TagHelperAttributeList? attributes = null,
	                                                           TagHelperContent? childContent = null)
	{
		ThrowIfNotInitialized();

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
			       : GenerateWrapper(labelOutput);
	}

	protected virtual void ApplyBaseClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output,
		             Options.LabelClasses,
		             Options.LabelValidClasses,
		             Options.LabelErrorClasses);
	}

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output,
		             Options.LabelWrapperClasses,
		             Options.LabelWrapperValidClasses,
		             Options.LabelWrapperErrorClasses);
	}
}