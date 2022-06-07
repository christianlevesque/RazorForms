namespace RazorForms.Templates.Elements;

public abstract class ErrorListBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput
{
	protected string GenerateErrorWrapperClasses()
		=> GenerateClasses(Model.Options.ErrorWrapperClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);

	protected string GenerateErrorClasses()
		=> GenerateClasses(Model.Options.ErrorClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);
}