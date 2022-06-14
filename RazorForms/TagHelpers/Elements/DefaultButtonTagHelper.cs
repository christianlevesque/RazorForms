using Microsoft.AspNetCore.Mvc.Rendering;
using RazorForms.Generators.Elements;
using RazorForms.Options.Elements;

namespace RazorForms.TagHelpers.Elements;

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