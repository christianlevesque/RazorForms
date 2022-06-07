using RazorForms.Options;

namespace RazorForms.Templates;

public abstract class TextInputBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	protected string GenerateComponentWrapperClasses()
		=> GenerateClasses(InputOptions.ComponentWrapperClasses,
		                   InputOptions.ComponentWrapperValidClasses,
		                   InputOptions.ComponentWrapperErrorClasses);

	protected string GenerateInputBlockWrapperClasses()
		=> GenerateClasses(InputOptions.InputBlockWrapperClasses,
		                   InputOptions.InputBlockWrapperValidClasses,
		                   InputOptions.InputBlockWrapperErrorClasses);
}