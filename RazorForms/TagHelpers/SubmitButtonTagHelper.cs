using Microsoft.AspNetCore.Mvc.Rendering;
using RazorForms.Generators.Buttons;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public class SubmitButtonTagHelper : ButtonHelperBase
{
	/// <inheritdoc />
	public SubmitButtonTagHelper(ISubmitButtonGenerator generator,
	                             IButtonOptions options) : base(generator,
	                                                            options,
	                                                            ButtonType.Submit)
	{
	}
}