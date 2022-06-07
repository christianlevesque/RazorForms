namespace RazorForms.Templates.Elements;

public abstract class ErrorListBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput
{
	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;ul&gt; containing error messages
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateErrorWrapperClasses()
		=> GenerateClasses(Model.Options.ErrorWrapperClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);

	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;li&gt; representing individiaul error messages
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateErrorClasses()
		=> GenerateClasses(Model.Options.ErrorClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);
}