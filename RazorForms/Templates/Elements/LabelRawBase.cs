using RazorForms.Options;

namespace RazorForms.Templates.Elements;

public abstract class LabelRawBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	protected string GenerateLabelClasses()
		=> GenerateClasses(InputOptions.LabelClasses,
		                   InputOptions.LabelValidClasses,
		                   InputOptions.LabelErrorClasses);
}