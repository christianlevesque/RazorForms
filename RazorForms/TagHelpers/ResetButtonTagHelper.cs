using Microsoft.AspNetCore.Mvc.Rendering;
using RazorForms.Generators.Buttons;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

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