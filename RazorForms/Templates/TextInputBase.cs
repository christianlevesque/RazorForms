namespace RazorForms.Templates;

public abstract class TextInputBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput
{
	protected string GenerateComponentWrapperClasses()
		=> GenerateClasses(Model.Options.ComponentWrapperClasses,
		                   Model.Options.ComponentWrapperValidClasses,
		                   Model.Options.ComponentWrapperErrorClasses);

	protected string GenerateInputBlockWrapperClasses()
		=> GenerateClasses(Model.Options.InputBlockWrapperClasses,
		                   Model.Options.InputBlockWrapperValidClasses,
		                   Model.Options.InputBlockWrapperErrorClasses);
}