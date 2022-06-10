using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Generators;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public class RazorFormsTagHelperBase : TagHelper
{
	private bool? _isValid;
	private bool? _isInvalid;

	protected const string ForAttributeName = "asp-for";

	protected readonly IOutputGenerator<IFormComponentOptions> InputGenerator;
	protected readonly ILabelGenerator LabelGenerator;
	protected readonly IInputBlockWrapperGenerator InputBlockWrapperGenerator;

	public readonly IHtmlGenerator Generator;
	public readonly IInputOptions Options;

	protected bool IsValid
	{
		get
		{
			if (_isValid.HasValue)
			{
				return _isValid.Value;
			}

			if (ViewContext == null)
			{
				return false;
			}

			if (For == null)
			{
				return false;
			}

			_isValid = ViewContext.ModelState.GetFieldValidationState(For.Name) == ModelValidationState.Valid;

			return _isValid.Value;
		}
	}

	protected bool IsInvalid
	{
		get
		{
			if (_isInvalid.HasValue)
			{
				return _isInvalid.Value;
			}

			if (ViewContext == null)
			{
				return false;
			}

			if (For == null)
			{
				return false;
			}

			_isInvalid = ViewContext.ModelState.GetFieldValidationState(For.Name) == ModelValidationState.Invalid;

			return _isInvalid.Value;
		}
	}

    [HtmlAttributeNotBound]
    [ViewContext]
	public ViewContext? ViewContext { get; set; }

	[HtmlAttributeName(ForAttributeName)]
	public ModelExpression? For { get; set; }

	/// <inheritdoc />
	protected RazorFormsTagHelperBase(IHtmlGenerator generator,
	                                  IInputOptions options,
	                                  IInputBlockWrapperGenerator inputBlockWrapperGenerator,
	                                  ILabelGenerator labelGenerator,
	                                  IOutputGenerator<IFormComponentOptions> inputGenerator)
	{
		Generator = generator;
		Options = options;
		InputBlockWrapperGenerator = inputBlockWrapperGenerator;
		LabelGenerator = labelGenerator;
		InputGenerator = inputGenerator;
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
		InputBlockWrapperGenerator.Init(Options, IsValid, IsInvalid);
		var wrapper = await InputBlockWrapperGenerator.GenerateOutput(context, this);

		// Generate label
		LabelGenerator.Init(Options, IsValid, IsInvalid);
		var label = await LabelGenerator.GenerateOutput(context, this);

		// Generate input
		InputGenerator.Init(Options, IsValid, IsInvalid);
		var input = await InputGenerator.GenerateOutput(context, this, attributesList, await output.GetChildContentAsync());

		// TODO: Generate errors
		output.PostContent.AppendHtml("<p>No errors here</p>");

		if (Options.InputFirst ?? false)
		{
			wrapper.PreContent.SetHtmlContent(InputGenerator.Render(input));
			wrapper.PostContent.SetHtmlContent(LabelGenerator.Render(label));
		}
		else
		{
			wrapper.PreContent.SetHtmlContent(LabelGenerator.Render(label));
			wrapper.PostContent.SetHtmlContent(InputGenerator.Render(input));
		}

		output.Content.SetHtmlContent(InputBlockWrapperGenerator.Render(wrapper));
	}
}