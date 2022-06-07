using System.Linq;

namespace RazorForms.Templates.Inputs;

public abstract class InputRawBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput
{
	protected string GenerateInputClasses()
	{
		var classAttribute = Model.Attributes.FirstOrDefault(a => a.Name == "class");
		var classValue = classAttribute?.Value.ToString() ?? string.Empty;

		return GenerateClasses(Model.Options.InputClasses,
		                       Model.Options.InputValidClasses,
		                       Model.Options.InputErrorClasses,
		                       startingValue: classValue);
	}
}