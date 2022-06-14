using Microsoft.AspNetCore.Mvc.Rendering;
using RazorForms.Generators.Elements;
using RazorForms.Options.Elements;

namespace RazorForms.TagHelpers.Elements;

public class ResetButtonTagHelper : ButtonHelperBase
{
	/// <inheritdoc />
	public ResetButtonTagHelper(IResetButtonGenerator generator,
	                            IButtonOptions options) : base(generator,
	                                                           options, 
	                                                           ButtonType.Reset)
	{
	}
}