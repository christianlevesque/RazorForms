namespace RazorForms.Templates.Elements;

public abstract class LabelRawBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput
{
	protected string GenerateLabelClasses()
		=> GenerateClasses(Model.Options.LabelClasses,
		                   Model.Options.LabelValidClasses,
		                   Model.Options.LabelErrorClasses);
}