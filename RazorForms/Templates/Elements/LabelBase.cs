using RazorForms.Options;

namespace RazorForms.Templates.Elements;

public abstract class LabelBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	protected string GenerateLabelWrapperClasses()
		=> GenerateClasses(InputOptions.LabelWrapperClasses,
		                   InputOptions.LabelWrapperValidClasses,
		                   InputOptions.LabelWrapperErrorClasses);
}