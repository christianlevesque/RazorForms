using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Generators;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public class SelectInputTagHelper : RazorFormsTagHelperBase
{
	protected const string ItemsAttributeName = "asp-items";

	[HtmlAttributeName(ItemsAttributeName)]
	public IEnumerable<SelectListItem>? Items { get; set; }

	/// <inheritdoc />
	public SelectInputTagHelper(IHtmlGenerator generator,
	                            ISelectOptions options,
	                            IInputBlockWrapperGenerator inputBlockWrapperGenerator,
	                            ILabelGenerator labelGenerator,
	                            ISelectGenerator selectGenerator) : base(generator,
	                                                                     options,
	                                                                     inputBlockWrapperGenerator,
	                                                                     labelGenerator,
	                                                                     selectGenerator)
	{
	}
}