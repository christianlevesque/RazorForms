using Microsoft.AspNetCore.Html;

namespace RazorForms.Models;

public class PartialMarkupModel
{
	public IHtmlContent? Content { get; }
	public string WrapperClasses { get; }
	public bool RemoveWrappers { get; }

	public PartialMarkupModel(
		IHtmlContent? content,
		string wrapperClasses,
		bool removeWrappers)
	{
		Content = content;
		WrapperClasses = wrapperClasses;
		RemoveWrappers = removeWrappers;
	}
}