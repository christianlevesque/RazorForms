using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Models;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

/// <summary>
/// Adds common functionality for use among all RazorForms tag helpers
/// </summary>
/// <typeparam name="TModel">The type of the razor component model</typeparam>
/// <typeparam name="TOptions">The type of the options object</typeparam>
public abstract class TagHelperBase<TModel, TOptions> : TagHelper
	where TModel : MarkupModel<TOptions>, new()
	where TOptions : FormComponentOptions, new()
{
	protected readonly IHtmlGenerator HtmlGenerator;
	protected readonly IHtmlHelper HtmlHelper;
	protected readonly TOptions Options;

	protected readonly Func<bool, HtmlEncoder, Task<TagHelperContent>> DefaultTagHelperContent =
		(a, b) => Task.FromResult((TagHelperContent)new DefaultTagHelperContent());

	/// <summary>
	/// Whether the &lt;label&gt; should receive the child content of the tag helper or not
	/// </summary>
	protected bool LabelReceivesChildContent { get; set; }

	/// <summary>
	/// The HTML element to wrap the entire tag helper output with
	/// </summary>
	protected string ContainerTag { get; set; } = "";

	/// <summary>
	/// The <see cref="TagMode"/> to use for the tag helper output
	/// </summary>
	protected TagMode ContainerTagMode { get; set; } = TagMode.StartTagAndEndTag;

	/// <summary>
	/// The HTML element to wrap the input tag helper output with
	/// </summary>
	protected string InputTag { get; set; } = "input";

	/// <summary>
	/// The <see cref="TagMode"/> to use for the input tag helper output
	/// </summary>
	protected TagMode InputTagMode { get; set; } = TagMode.SelfClosing;

	[HtmlAttributeName("asp-for")]
	public ModelExpression For { get; set; } = default!;

	[HtmlAttributeName("template-path")]
	public string? TemplatePath { get; set; }

	[HtmlAttributeNotBound]
	[ViewContext]
	public ViewContext ViewContext { get; set; } = default!;

	/// <inheritdoc />
	protected TagHelperBase(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		TOptions options)
	{
		HtmlGenerator = htmlGenerator;
		HtmlHelper = htmlHelper;
		Options = options;
	}

	/// <inheritdoc />
	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		// Set up the output wrapper
		output.TagName = ContainerTag;
		output.TagMode = ContainerTagMode;

		// Set up model
		var model = await GenerateHtmlModel(context, output);
		ProcessModel(model);

		// Render content
		(HtmlHelper as IViewContextAware)!.Contextualize(ViewContext);
		var content = await HtmlHelper.PartialAsync(
			TemplatePath ?? Options.TemplatePath,
			model);
		output.Content.SetHtmlContent(content);
	}

#region Model generation and manipulation

	/// <summary>
	/// Creates the <see cref="TModel"/> to be used by the Razor template
	/// </summary>
	/// <param name="context">The <see cref="TagHelperContext"/></param>
	/// <param name="output">The <see cref="TagHelperOutput"/> of the root element</param>
	/// <returns></returns>
	protected async Task<TModel> GenerateHtmlModel(TagHelperContext context, TagHelperOutput output)
	{
		// Set up the viewmodel to send to the Razor template
		var model = new TModel
		{
			LabelHtml = await CreateLabel(context, output),
			ElementOptions = new TOptions()
		};

		if (!Options.RenderInputInsideLabel)
		{
			model.InputHtml = await CreateInput(context, output);
		}

		SetupModelOptions(model.ElementOptions);
		AddCssClasses(model, output.Attributes);

		return model;
	}

	/// <summary>
	/// Runs any additional processing on the model (e.g., processing additional properties for a child class of <see cref="MarkupModel{TOptions}"/>)
	/// </summary>
	/// <param name="model">The model to process</param>
	protected virtual void ProcessModel(TModel model)
	{
	}

	/// <summary>
	/// Transposes options from the class <see cref="TOptions"/> implementation to the one on the <see cref="MarkupModel{TOptions}"/>
	/// </summary>
	/// <param name="options">The <see cref="MarkupModel{TOptions}.ElementOptions"/> instance</param>
	protected virtual void SetupModelOptions(TOptions options)
	{
		options.InputFirst = Options.InputFirst;
		options.RemoveWrappers = Options.RemoveWrappers;
	}

#endregion

#region Tag helper creation

	/// <summary>
	/// Creates the &lt;label&gt; tag
	/// </summary>
	/// <param name="context">The <see cref="TagHelperContext"/></param>
	/// <param name="output">The <see cref="TagHelperOutput"/> of the root element</param>
	/// <returns>The <see cref="TagHelperOutput"/> containing the &lt;label&gt;</returns>
	protected virtual async Task<TagHelperOutput> CreateLabel(
		TagHelperContext context,
		TagHelperOutput output)
	{
		var labelHelper = new LabelTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For
		};

		var labelOutput = new TagHelperOutput(
			"label",
			new TagHelperAttributeList(),
			DefaultTagHelperContent);

		AddCustomLabelAttributes(labelOutput.Attributes);

		TagHelperContent labelChildContent = new DefaultTagHelperContent();
		var providedChildContent = await output.GetChildContentAsync();
		if (providedChildContent.IsEmptyOrWhiteSpace)
		{
			if (!string.IsNullOrEmpty(For.Metadata.DisplayName))
			{
				providedChildContent.AppendHtml(
					Utilities.GenerateLabelText(Options, For.Metadata.DisplayName));
			}
			else
			{
				providedChildContent.AppendHtml(
					Utilities.GenerateLabelText(Options, For.Metadata.Name!));
			}
		}

		if (Options.RenderInputInsideLabel)
		{
			var inputContent = await CreateInput(context, output);

			if (LabelReceivesChildContent)
			{
				if (Options.InputFirst)
				{
					labelChildContent = labelChildContent
						.AppendHtml(inputContent)
						.AppendHtml(providedChildContent);
				}
				else
				{
					labelChildContent = labelChildContent
						.AppendHtml(providedChildContent)
						.AppendHtml(inputContent);
				}
			}
			else
			{
				var labelHtml = For.Metadata.DisplayName is null
					? ""
					: Utilities.GenerateLabelText(Options, For.Metadata.DisplayName!);

				if (Options.InputFirst)
				{
					labelChildContent = labelChildContent
						.AppendHtml(inputContent)
						.AppendHtml(labelHtml);
				}
				else
				{
					labelChildContent = labelChildContent
						.AppendHtml(labelHtml)
						.AppendHtml(inputContent);
				}
			}

			labelOutput.Content.SetHtmlContent(labelChildContent);
		}
		else if (LabelReceivesChildContent)
		{
			labelChildContent = labelChildContent.AppendHtml(providedChildContent);
			labelOutput.Content.SetHtmlContent(labelChildContent);
		}

		ApplyCssClassesToLabel(labelOutput);

		await labelHelper.ProcessAsync(context, labelOutput);
		return labelOutput;
	}

	/// <summary>
	/// Creates the &lt;input&gt; tag with pre-selected attributes
	/// </summary>
	/// <param name="context">The <see cref="TagHelperContext"/></param>
	/// <param name="output">The <see cref="TagHelperOutput"/> of the root element</param>
	/// <returns></returns>
	protected async Task<TagHelperOutput> CreateInput(
		TagHelperContext context,
		TagHelperOutput output)
	{
		var attributes = Utilities.GetInputAttributes(output.Attributes);
		var tagHelper = CreateInputTagHelper();

		var inputOutput = new TagHelperOutput(
			InputTag,
			attributes,
			DefaultTagHelperContent)
		{
			TagMode = InputTagMode
		};

		AddCustomInputAttributes(inputOutput.Attributes);

		if (!LabelReceivesChildContent)
		{
			inputOutput.Content.SetHtmlContent(await output.GetChildContentAsync());
		}

		tagHelper?.Init(context);

		ApplyCssClassesToInput(inputOutput);

		if (tagHelper is not null)
		{
			await tagHelper.ProcessAsync(context, inputOutput);
		}

		return inputOutput;
	}

	/// <summary>
	/// Adds additional attributes to the label's output
	/// </summary>
	/// <param name="attributes">The label attributes</param>
	protected virtual void AddCustomLabelAttributes(TagHelperAttributeList attributes)
	{
	}

	/// <summary>
	/// Adds additional attributes to the input's output
	/// </summary>
	/// <param name="attributes">The input attributes</param>
	protected virtual void AddCustomInputAttributes(TagHelperAttributeList attributes)
	{
	}

	/// <summary>
	/// Creates the HTML tag that captures user input (&lt;Input&gt;, &lt;select&gt;, etc.)
	/// </summary>
	/// <returns></returns>
	protected virtual TagHelper? CreateInputTagHelper() => null;

#endregion

#region CSS generation

	/// <summary>
	/// Applies CSS classes to the &lt;input&gt; tag
	/// </summary>
	/// <param name="input">The <see cref="TagHelperOutput"/> for the &lt;input&gt; tag</param>
	protected virtual void ApplyCssClassesToInput(TagHelperOutput input)
	{
		Utilities.AddClassesToOutput(input, Options.InputClasses);
	}

	/// <summary>
	/// Applies CSS classes to the &lt;label&gt; tag
	/// </summary>
	/// <param name="label">The <see cref="TagHelperOutput"/> for the &lt;label&gt; tag</param>
	protected virtual void ApplyCssClassesToLabel(TagHelperOutput label)
	{
		Utilities.AddClassesToOutput(label, Options.LabelClasses);
	}

	/// <summary>
	/// Adds CSS classes to the <see cref="MarkupModel{TOptions}"/> based on the provided options
	/// </summary>
	/// <param name="model">The <see cref="MarkupModel{TOptions}"/> to add classes to</param>
	/// <param name="attributeList">The <see cref="TagHelperAttributeList"/> that contains potential component wrapper CSS classes</param>
	protected virtual void AddCssClasses(MarkupModel<TOptions> model, TagHelperAttributeList attributeList)
	{
		var classAttribute = attributeList.FirstOrDefault(a => a.Name == "class");
		model.ElementOptions.ComponentWrapperClasses =
			Utilities.MergeCssStrings(classAttribute?.Value.ToString(), Options.ComponentWrapperClasses);
		model.ElementOptions.InputWrapperClasses = Options.InputWrapperClasses;
		model.ElementOptions.LabelWrapperClasses = Options.LabelWrapperClasses;
	}

#endregion
}