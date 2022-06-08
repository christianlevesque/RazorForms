namespace RazorForms.Options;

/// <summary>
/// Defines options required by all components that support data validation
/// </summary>
public interface IComponentWithValidationOptions : IComponentOptions
{
	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component when model validation succeeds
	/// </summary>
	public string? ComponentWrapperValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component when model validation fails
	/// </summary>
	public string? ComponentWrapperErrorClasses { get; set; }
}