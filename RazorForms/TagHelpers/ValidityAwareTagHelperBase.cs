using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Generators;
using RazorForms.Generators.Elements;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public abstract class ValidityAwareTagHelperBase : RazorFormsTagHelperBase
{
	private bool? _isValid;
	private bool? _isInvalid;

	protected bool IsValid
	{
		get
		{
			if (_isValid.HasValue)
			{
				return _isValid.Value;
			}

			if (ViewContext == null || For == null)
			{
				return (_isValid = false).Value;
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

			if (ViewContext == null || For == null)
			{
				return (_isInvalid = false).Value;
			}

			_isInvalid = ViewContext.ModelState.GetFieldValidationState(For.Name) == ModelValidationState.Invalid;

			return _isInvalid.Value;
		}
	}

	public readonly IFormComponentWithValidationOptions Options;
	protected readonly IValidityAwareOutputGenerator<IFormComponentWithValidationOptions> InputGenerator;
	protected readonly ILabelGenerator LabelGenerator;
	protected readonly IInputBlockWrapperGenerator InputBlockWrapperGenerator;
	protected readonly IErrorGenerator ErrorGenerator;

	/// <inheritdoc />
	protected ValidityAwareTagHelperBase(IHtmlGenerator generator,
	                                     IFormComponentWithValidationOptions options,
	                                     IInputBlockWrapperGenerator inputBlockWrapperGenerator,
	                                     ILabelGenerator labelGenerator,
	                                     IValidityAwareOutputGenerator<IFormComponentWithValidationOptions> inputGenerator,
	                                     IErrorGenerator errorGenerator) : base(generator)
	{
		Options = options;
		InputBlockWrapperGenerator = inputBlockWrapperGenerator;
		LabelGenerator = labelGenerator;
		InputGenerator = inputGenerator;
		ErrorGenerator = errorGenerator;
	}

	/// <inheritdoc />
	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		ThrowIfForNull();

		output.TagName = "div";
		output.TagMode = TagMode.StartTagAndEndTag;

		// Set up attributes and prepare them to be passed to the child <input>
		var attributesList = ProcessAttributes(output);

		if (!string.IsNullOrEmpty(Options.ComponentWrapperClasses))
		{
			output.AddClass(Options.ComponentWrapperClasses, HtmlEncoder.Default);
		}

		// Set up output generation
		var childContent = await output.GetChildContentAsync();
		if (childContent.IsEmptyOrWhiteSpace && !string.IsNullOrEmpty(For!.Metadata.DisplayName))
		{
			childContent.AppendHtml(Utilities.GenerateLabelText(Options, For!.Metadata.DisplayName));
		}

		// Generate wrapper
		InputBlockWrapperGenerator.Init(Options, IsValid, IsInvalid);
		var wrapper = await InputBlockWrapperGenerator.GenerateOutput(context, this);

		// Generate input
		InputGenerator.Init(Options, IsValid, IsInvalid);
		var input = await InputGenerator.GenerateOutput(context,
		                                                this,
		                                                attributesList,
		                                                LabelReceivesChildContent ? null : childContent);

		// Generate label
		LabelGenerator.Init(Options, IsValid, IsInvalid);

		TagHelperContent labelChildContent = new DefaultTagHelperContent();
		if (Options.RenderInputInsideLabel ?? false)
		{
			if (LabelReceivesChildContent)
			{
				// If the label receives the child content, everything will work as we expect
				// because we manually added the display name text to the child content above
				if (Options.InputFirst ?? false)
				{
					labelChildContent = labelChildContent
						.AppendHtml(InputGenerator.Render(input))
						.AppendHtml(childContent);
				}
				else
				{
					labelChildContent = labelChildContent
						.AppendHtml(childContent)
					    .AppendHtml(InputGenerator.Render(input));
				}
			}
			else
			{
				// If the label DOESN'T receive the child content, we still need to manually add the display name text here
				var labelHtml = Utilities.GenerateLabelText(Options, For!.Metadata.DisplayName!);

				if (Options.InputFirst ?? false)
				{
					labelChildContent = labelChildContent
						.AppendHtml(InputGenerator.Render(input))
						.AppendHtml(labelHtml);
				}
				else
				{
					labelChildContent = labelChildContent
						.AppendHtml(labelHtml)
						.AppendHtml(InputGenerator.Render(input));
				}
			}
		}
		else if (LabelReceivesChildContent)
		{
			labelChildContent = labelChildContent.AppendHtml(childContent);
		}

		var label = await LabelGenerator.GenerateOutput(context,
		                                                this,
		                                                childContent: labelChildContent);

		ErrorGenerator.Init(Options, IsValid, IsInvalid, For!, ViewContext!);
		var errors = await ErrorGenerator.GenerateOutput(context);

		if (Options.RenderInputInsideLabel ?? false)
		{
			wrapper.PreContent.SetHtmlContent(LabelGenerator.Render(label));
		}
		else
		{
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
		}

		wrapper.PostElement.SetHtmlContent(ErrorGenerator.Render(errors));
		output.Content.SetHtmlContent(InputBlockWrapperGenerator.Render(wrapper));
	}
}