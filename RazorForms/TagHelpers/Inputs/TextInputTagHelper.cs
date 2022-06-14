using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using RazorForms.Generators;
using RazorForms.Generators.Elements;
using RazorForms.Generators.Inputs;
using RazorForms.Options.Inputs;

namespace RazorForms.TagHelpers.Inputs;

public class TextInputTagHelper : RazorFormsTagHelperBase
{
	protected const string FormatAttributeName = "asp-format";

	[HtmlAttributeName(FormatAttributeName)]
	public string? Format { get; set; }

	/// <inheritdoc />
	public TextInputTagHelper(IHtmlGenerator generator,
	                          IInputOptions options,
	                          IInputBlockWrapperGenerator inputBlockWrapperGenerator,
	                          ILabelGenerator labelGenerator,
	                          IInputGenerator inputGenerator,
	                          IErrorGenerator errorGenerator) : base(generator,
	                                                                 options,
	                                                                 inputBlockWrapperGenerator, 
	                                                                 labelGenerator, 
	                                                                 inputGenerator,
	                                                                 errorGenerator)
	{
	}
}