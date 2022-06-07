using RazorForms.Options;

namespace RazorForms.Templates.Inputs;

public abstract class InputBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;div&gt; surrounding the &lt;input&gt;
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateInputWrapperClasses()
		=> GenerateClasses(InputOptions.InputWrapperClasses,
		                   InputOptions.InputWrapperValidClasses,
		                   InputOptions.InputWrapperErrorClasses);
}