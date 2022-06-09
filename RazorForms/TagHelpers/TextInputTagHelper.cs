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
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public class TextInputTagHelper : TagHelper
{
	private ILogger<TextInputTagHelper> _logger;
	protected const string ForAttributeName = "asp-for";
	protected const string FormatAttributeName = "asp-format";

	public IHtmlGenerator Generator;
	public IInputOptions Options;

	protected bool IsValid
	{
		get
		{
			if (ViewContext == null)
			{
				return false;
			}

			if (For == null)
			{
				return false;
			}

			return ViewContext.ModelState.GetFieldValidationState(For.Name) == ModelValidationState.Valid;
		}
	}

	protected bool IsInvalid
	{
		get
		{
			if (ViewContext == null)
			{
				return false;
			}

			if (For == null)
			{
				return false;
			}

			return ViewContext.ModelState.GetFieldValidationState(For.Name) == ModelValidationState.Invalid;
		}
	}

    [HtmlAttributeNotBound]
    [ViewContext]
	public ViewContext? ViewContext { get; set; }

	[HtmlAttributeName(ForAttributeName)]
	public ModelExpression? For { get; set; }

	[HtmlAttributeName(FormatAttributeName)]
	public string? Format { get; set; }

	/// <inheritdoc />
	public TextInputTagHelper(IHtmlGenerator generator, IInputOptions options, ILogger<TextInputTagHelper> logger)
	{
		Generator = generator;
		Options = options;
		_logger = logger;
	}

	/// <inheritdoc />
	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		output.TagName = "div";
		output.TagMode = TagMode.StartTagAndEndTag;

		// Set up attributes and prepare them to be passed to the child <input>
		var attributes = output.Attributes.Where(a => a.Name != "class");
		var attributesList = new TagHelperAttributeList(attributes);
		output.Attributes.Clear();

		if (!string.IsNullOrEmpty(Options.ComponentWrapperClasses))
		{
			output.AddClass(Options.ComponentWrapperClasses, HtmlEncoder.Default);
		}

		// Generate wrapper
		var wrapperGenerator = new InputBlockWrapperGenerator(Options, IsValid, IsInvalid);
		var wrapper = await wrapperGenerator.GenerateOutput(context, this);

		// Generate label
		var labelGenerator = new LabelGenerator(Options, IsValid, IsInvalid);
		var label = await labelGenerator.GenerateOutput(context, this);

		// Generate input
		var inputGenerator = new InputGenerator(Options, IsValid, IsInvalid);
		var input = await inputGenerator.GenerateOutput(context, this, attributesList);

		// TODO: Generate errors
		output.PostContent.AppendHtml("<p>No errors here</p>");

		if (Options.InputFirst ?? false)
		{
			wrapper.PreContent.SetHtmlContent(inputGenerator.Render(input));
			wrapper.PostContent.SetHtmlContent(labelGenerator.Render(label));
		}
		else
		{
			wrapper.PreContent.SetHtmlContent(labelGenerator.Render(label));
			wrapper.PostContent.SetHtmlContent(inputGenerator.Render(input));
		}

		output.Content.SetHtmlContent(wrapperGenerator.Render(wrapper));
	}
}