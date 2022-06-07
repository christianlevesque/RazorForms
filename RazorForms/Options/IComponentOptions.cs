namespace RazorForms.Options;

/// <summary>
/// Defines options required by all components
/// </summary>
public interface IComponentOptions
{
	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component
	/// </summary>
	string? ComponentWrapperClasses { get; set; }

	/// <summary>
	/// Determines whether or not to remove the &lt;div&gt; surrounding &lt;label&gt; and &lt;input&gt; elements
	/// </summary>
	bool? RemoveWrappers { get; set; }
}