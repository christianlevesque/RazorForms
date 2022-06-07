using RazorForms.Options;

namespace RazorForms.Templates;

public abstract class TextInputBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;div&gt; containing the component
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateComponentWrapperClasses()
		=> GenerateClasses(InputOptions.ComponentWrapperClasses,
		                   InputOptions.ComponentWrapperValidClasses,
		                   InputOptions.ComponentWrapperErrorClasses);

	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;div&gt; containing the &lt;label&gt; and &lt;input&gt; elements
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateInputBlockWrapperClasses()
		=> GenerateClasses(InputOptions.InputBlockWrapperClasses,
		                   InputOptions.InputBlockWrapperValidClasses,
		                   InputOptions.InputBlockWrapperErrorClasses);
}