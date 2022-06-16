using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Elements;

public class CheckRadioInputSectionGenerator : ValidityAwareOutputGenerator<IFormComponentWithValidationOptions>, ICheckRadioInputSectionGenerator
{
	/// <inheritdoc />
	public override Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     RazorFormsTagHelperBase helper,
	                                                     TagHelperAttributeList? attributes = null,
	                                                     TagHelperContent? childContent = null)
	{
		ThrowIfNotInitialized();

		if (childContent == null)
		{
			throw new ArgumentNullException(nameof(childContent), "Checkbox and radio groups must have child content");
		}

		var output = new TagHelperOutput(tagName: "div",
		                                 attributes: attributes,
		                                 getChildContentAsync: DefaultTagHelperContent);

		output.Content.SetHtmlContent(childContent);
		ApplyWrapperClasses(output);
		return Task.FromResult(output);
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