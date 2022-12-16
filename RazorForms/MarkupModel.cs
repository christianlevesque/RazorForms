using Microsoft.AspNetCore.Html;

namespace RazorForms;

public class MarkupModel
{
	public IHtmlContent InputHtml { get; set; } = default!;
	public IHtmlContent LabelHtml { get; set; } = default!;
}