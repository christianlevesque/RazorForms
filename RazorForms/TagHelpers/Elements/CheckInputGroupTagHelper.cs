using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorForms.Generators.Elements;
using RazorForms.Options.Inputs;

namespace RazorForms.TagHelpers.Elements;

public class CheckInputGroupTagHelper : ValidityAwareTagHelperBase
{
	/// <inheritdoc />
	public CheckInputGroupTagHelper(IHtmlGenerator generator,
	                                ICheckInputGroupOptions options,
	                                IInputBlockWrapperGenerator inputBlockWrapperGenerator,
	                                ILabelGenerator labelGenerator,
	                                ICheckRadioInputSectionGenerator inputGenerator,
	                                IErrorGenerator errorGenerator) : base(generator,
	                                                                       options,
	                                                                       inputBlockWrapperGenerator,
	                                                                       labelGenerator,
	                                                                       inputGenerator,
	                                                                       errorGenerator)
	{
	}
}