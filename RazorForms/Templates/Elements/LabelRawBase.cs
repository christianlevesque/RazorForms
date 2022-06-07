namespace RazorForms.Templates.Elements;

public abstract class LabelRawBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput
{
	/// <summary>
	/// Generates the appropriate CSS classes for the &lt;label&gt;
	/// </summary>
	/// <returns>a string representing the CSS classes</returns>
	protected string GenerateLabelClasses()
		=> GenerateClasses(Model.Options.LabelClasses,
		                   Model.Options.LabelValidClasses,
		                   Model.Options.LabelErrorClasses);
}