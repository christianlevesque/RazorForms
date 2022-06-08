using RazorForms.Options;

namespace RazorForms.Templates.Elements;

public abstract class LabelBase<TModel> : GeneralInputBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;div&gt; surrounding the &lt;label&gt;
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateLabelWrapperClasses()
		=> GenerateClasses(InputOptions.LabelWrapperClasses,
		                   InputOptions.LabelWrapperValidClasses,
		                   InputOptions.LabelWrapperErrorClasses);
}