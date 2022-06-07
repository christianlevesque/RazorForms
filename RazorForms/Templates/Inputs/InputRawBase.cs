using System.Linq;
using RazorForms.Options;

namespace RazorForms.Templates.Inputs;

public abstract class InputRawBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	protected string GenerateInputClasses()
	{
		var classAttribute = Model.Attributes.FirstOrDefault(a => a.Name == "class");
		var classValue = classAttribute?.Value.ToString() ?? string.Empty;

		return GenerateClasses(InputOptions.InputClasses,
		                       InputOptions.InputValidClasses,
		                       InputOptions.InputErrorClasses,
		                       startingValue: classValue);
	}
}