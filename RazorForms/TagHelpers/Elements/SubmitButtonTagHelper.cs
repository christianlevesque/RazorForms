using Microsoft.AspNetCore.Mvc.Rendering;
using RazorForms.Generators.Elements;
using RazorForms.Options.Elements;

namespace RazorForms.TagHelpers.Elements;

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