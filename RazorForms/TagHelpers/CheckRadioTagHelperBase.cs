using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Generators.Elements;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public abstract class CheckRadioTagHelperBase<TGenerator> : ValidityUnawareTagHelperBase<TGenerator>
{
	/// <inheritdoc />
	public CheckRadioTagHelperBase(IHtmlGenerator generator,
	                               IFormComponentOptions options,
	                               IInputBlockWrapperGenerator wrapperGenerator,
	                               ILabelGenerator labelGenerator,
	                               TGenerator inputGenerator) : base(generator,
	                                                                 options,
	                                                                 wrapperGenerator,
	                                                                 labelGenerator,
	                                                                 inputGenerator)
	{
	}

	protected abstract void AddCheckedAttributeIfAppropriate(TagHelperAttributeList attributes);
}