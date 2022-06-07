using RazorForms.Options;

namespace RazorForms.Templates.Elements;

public abstract class ErrorListBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	protected string GenerateErrorWrapperClasses()
		=> GenerateClasses(InputOptions.ErrorWrapperClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);

	protected string GenerateErrorClasses()
		=> GenerateClasses(InputOptions.ErrorClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);
}