using Microsoft.AspNetCore.Mvc.Rendering;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public class ResetButtonTagHelper : ButtonHelperBase
{
	/// <inheritdoc />
	public ResetButtonTagHelper(IHtmlHelper html, IButtonOptions options) : base(html, options)
	{
		Type = ButtonType.Reset;
	}
}