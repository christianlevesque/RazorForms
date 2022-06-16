using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Elements;

public class LabelGenerator : ValidityAwareOutputGenerator<IFormComponentOptions>, ILabelGenerator
{
	/// <inheritdoc />
	public override async Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                           RazorFormsTagHelperBase helper,
	                                                           TagHelperAttributeList? attributes = null,
	                                                           TagHelperContent? childContent = null)
	{
		ThrowIfNotInitialized();

		attributes ??= new TagHelperAttributeList();

		var labelHelper = new LabelTagHelper(helper.Generator)
		{
			ViewContext = helper.ViewContext,
			For = helper.For
		};

		var labelOutput = new TagHelperOutput(tagName: "label",
		                                      attributes: attributes,
		                                      getChildContentAsync: DefaultTagHelperContent);

		if (childContent != null && !childContent.IsEmptyOrWhiteSpace)
		{
			labelOutput.Content.SetHtmlContent(childContent.GetContent());
		}

		ApplyBaseClasses(labelOutput);

		await labelHelper.ProcessAsync(context, labelOutput);

		return Options.RemoveWrappers ?? false
			       ? labelOutput
			       : GenerateWrapper(labelOutput);
	}

	protected virtual void ApplyBaseClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		if (Options is IFormComponentWithValidationOptions withValidationOptions)
		{
			ApplyClasses(output,
		                 withValidationOptions.LabelClasses,
		                 withValidationOptions.LabelValidClasses,
		                 withValidationOptions.LabelErrorClasses);
		}
		else
		{
			ApplyClasses(output, Options.LabelClasses);
		}
	}

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		if (Options is IFormComponentWithValidationOptions withValidationOptions)
		{
			ApplyClasses(output,
			             withValidationOptions.LabelWrapperClasses,
			             withValidationOptions.LabelWrapperValidClasses,
			             withValidationOptions.LabelWrapperErrorClasses);
		}
		else
		{
			ApplyClasses(output, Options.LabelWrapperClasses);
		}
	}
}