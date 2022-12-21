using Microsoft.AspNetCore.Html;

namespace RazorForms.Models;

/// <summary>
/// The model used when rendering &lt;label&gt; or &lt;nput&gt; tag helpers
/// </summary>
public class WrappedContentModel
{
	/// <summary>
	/// The content to render
	/// </summary>
	public IHtmlContent? Content { get; }

	/// <summary>
	/// Any classes that should be applied to the &lt;div&gt; wrapper
	/// </summary>
	public string WrapperClasses { get; }

	/// <summary>
	/// Whether or not to remove the &lt;div&gt; wrapper
	/// </summary>
	public bool RemoveWrappers { get; }

	/// <summary>
	/// Creates a new instance of the model
	/// </summary>
	/// <param name="content">Passed to <see cref="Content"/></param>
	/// <param name="wrapperClasses">Passed to <see cref="WrapperClasses"/></param>
	/// <param name="removeWrappers">Passed to <see cref="RemoveWrappers"/></param>
	public WrappedContentModel(
		IHtmlContent? content,
		string wrapperClasses,
		bool removeWrappers)
	{
		Content = content;
		WrapperClasses = wrapperClasses;
		RemoveWrappers = removeWrappers;
	}
}