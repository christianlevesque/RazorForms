using Microsoft.AspNetCore.Mvc.Rendering;
using RazorForms.Generators.Buttons;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public class DefaultButtonTagHelper : ButtonHelperBase
{
	/// <inheritdoc />
	public DefaultButtonTagHelper(IDefaultButtonGenerator generator,
	                              IButtonOptions options) : base(generator,
	                                                             options,
	                                                             ButtonType.Default)
	{
	}
}