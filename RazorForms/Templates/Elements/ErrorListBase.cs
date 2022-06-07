using RazorForms.Options;

namespace RazorForms.Templates.Elements;

public abstract class ErrorListBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;ul&gt; containing error messages
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateErrorWrapperClasses()
		=> GenerateClasses(InputOptions.ErrorWrapperClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);

	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;li&gt; representing individiaul error messages
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateErrorClasses()
		=> GenerateClasses(InputOptions.ErrorClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);
}