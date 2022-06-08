using Microsoft.AspNetCore.Mvc.Rendering;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public class DefaultButtonTagHelper : ButtonHelperBase
{
	/// <inheritdoc />
	public DefaultButtonTagHelper(IHtmlHelper html, IButtonOptions options) : base(html, options)
	{
		Type = ButtonType.Default;
	}
}