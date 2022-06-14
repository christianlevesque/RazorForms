using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorForms.Generators;
using RazorForms.Generators.Elements;
using RazorForms.Generators.Inputs;
using RazorForms.Options.Inputs;

namespace RazorForms.TagHelpers.Inputs;

public class TextAreaInputTagHelper : RazorFormsTagHelperBase
{
	/// <inheritdoc />
	public TextAreaInputTagHelper(IHtmlGenerator generator,
                                  ITextAreaOptions options,
                                  IInputBlockWrapperGenerator inputBlockWrapperGenerator,
                                  ILabelGenerator labelGenerator,
                                  ITextAreaGenerator inputGenerator,
                                  IErrorGenerator errorGenerator) : base(generator,
                                                                         options,
                                                                         inputBlockWrapperGenerator,
                                                                         labelGenerator,
                                                                         inputGenerator,
                                                                         errorGenerator)
	{
	}
}