using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using RazorForms.Generators;
using RazorForms.Generators.Elements;
using RazorForms.Generators.Inputs;
using RazorForms.Options.Inputs;

namespace RazorForms.TagHelpers.Inputs;

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
	                            ISelectGenerator selectGenerator,
	                            IErrorGenerator errorGenerator) : base(generator,
	                                                                   options,
	                                                                   inputBlockWrapperGenerator,
	                                                                   labelGenerator,
	                                                                   selectGenerator,
	                                                                   errorGenerator)
	{
	}
}