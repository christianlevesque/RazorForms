using RazorForms.Options;

namespace RazorForms.Templates.Elements;

public abstract class LabelRawBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;label&gt;
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateLabelClasses()
		=> GenerateClasses(InputOptions.LabelClasses,
		                   InputOptions.LabelValidClasses,
		                   InputOptions.LabelErrorClasses);
}