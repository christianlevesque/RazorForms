using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorForms.Options;

namespace RazorForms.Generators.Elements;

public interface IErrorGenerator : IOutputGeneratorWithValidity<IFormComponentOptions>
{
	void Init(IFormComponentOptions options,
	          bool isValid,
	          bool isInvalid,
	          ModelExpression modelExpression,
	          ViewContext viewContext);
}