namespace RazorForms.Templates;

public abstract class TextInputBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput
{
	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;div&gt; containing the component
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateComponentWrapperClasses()
		=> GenerateClasses(Model.Options.ComponentWrapperClasses,
		                   Model.Options.ComponentWrapperValidClasses,
		                   Model.Options.ComponentWrapperErrorClasses);

	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;div&gt; containing the &lt;label&gt; and &lt;input&gt; elements
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateInputBlockWrapperClasses()
		=> GenerateClasses(Model.Options.InputBlockWrapperClasses,
		                   Model.Options.InputBlockWrapperValidClasses,
		                   Model.Options.InputBlockWrapperErrorClasses);
}