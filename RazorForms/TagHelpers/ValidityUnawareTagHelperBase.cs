using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorForms.Generators.Elements;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public abstract class ValidityUnawareTagHelperBase<TOutputGenerator> : RazorFormsTagHelperBase
{
	protected readonly IFormComponentOptions Options;
	protected readonly IInputBlockWrapperGenerator WrapperGenerator;
	protected readonly ILabelGenerator LabelGenerator;
	protected readonly TOutputGenerator InputGenerator;

	protected ValidityUnawareTagHelperBase(IHtmlGenerator generator,
	                                       IFormComponentOptions options,
	                                       IInputBlockWrapperGenerator wrapperGenerator,
	                                       ILabelGenerator labelGenerator,
	                                       TOutputGenerator inputGenerator) : base(generator)
	{
		Options = options;
		WrapperGenerator = wrapperGenerator;
		LabelGenerator = labelGenerator;
		InputGenerator = inputGenerator;
	}
}