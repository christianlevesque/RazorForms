namespace RazorForms;

public interface IFormMarkupSettings
{
	/// <summary>
	/// Determines whether or not to remove the &lt;div&gt; surrounding &lt;label&gt; and &lt;input&gt; elements
	/// </summary>
	bool? RemoveWrappers { get; set; }

	/// <summary>
	/// Determines whether the &lt;input&gt; should come first in the markup or not
	/// </summary>
	bool? InputFirst { get; set; }
}