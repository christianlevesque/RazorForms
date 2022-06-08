using Microsoft.AspNetCore.Mvc.Rendering;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public class SubmitButtonTagHelper : ButtonHelperBase
{
	/// <inheritdoc />
	public SubmitButtonTagHelper(IHtmlHelper html, IButtonOptions options) : base(html, options)
	{
		Type = ButtonType.Submit;
	}
}