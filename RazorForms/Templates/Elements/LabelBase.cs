namespace RazorForms.Templates.Elements;

public abstract class LabelBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput
{
	protected string GenerateLabelWrapperClasses()
		=> GenerateClasses(Model.Options.LabelWrapperClasses,
		                   Model.Options.LabelWrapperValidClasses,
		                   Model.Options.LabelWrapperErrorClasses);
}