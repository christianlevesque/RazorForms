using RazorForms.Options;

namespace RazorForms.Templates.Inputs;

public abstract class InputBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	protected string GenerateInputWrapperClasses()
		=> GenerateClasses(InputOptions.InputWrapperClasses,
		                   InputOptions.InputWrapperValidClasses,
		                   InputOptions.InputWrapperErrorClasses);
}