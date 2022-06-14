using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorForms.Options;

namespace RazorForms.Generators.Elements;

public interface IErrorGenerator : IOutputGenerator<IFormComponentWithValidationOptions>
{
	void Init(IFormComponentWithValidationOptions options,
	          bool isValid,
	          bool isInvalid,
	          ModelExpression modelExpression,
	          ViewContext viewContext);
}