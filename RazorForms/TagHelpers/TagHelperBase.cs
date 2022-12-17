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

public abstract class TagHelperBase<TModel, TOptions> : TagHelper
	where TModel : MarkupModel<TOptions>, new()
	where TOptions : FormComponentOptions, new()
{
	protected readonly IHtmlGenerator HtmlGenerator;
    protected readonly IHtmlHelper HtmlHelper;
    protected readonly TOptions Options;

    protected readonly Func<bool, HtmlEncoder, Task<TagHelperContent>> DefaultTagHelperContent =
	    (a, b) => Task.FromResult((TagHelperContent) new DefaultTagHelperContent());

	/// <summary>
	/// Whether the &lt;label&gt; should receive the child content of the tag helper or not
	/// </summary>
	protected bool LabelReceivesChildContent { get; set; }

	/// <summary>
	/// The HTML element to wrap the entire tag helper output with
	/// </summary>
	protected string OutputTag { get; set; } = "div";

	/// <summary>
	/// The <see cref="TagMode"/> to use for the tag helper output
	/// </summary>
	protected TagMode OutputTagMode { get; set; } = TagMode.StartTagAndEndTag;

	[HtmlAttributeName("asp-for")]
	public ModelExpression For { get; set; } = default!;

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
		var model = await GenerateHtmlModel(context, output);
		await ProcessModel(model);

		var content = await HtmlHelper.PartialAsync($"~/{Options.TemplatePath}.cshtml", model);
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
		// The IHtmlHelper isn't ready to use as-is
		(HtmlHelper as IViewContextAware)!.Contextualize(ViewContext);

		// Set up the output wrapper
		output.TagName = OutputTag;
		output.TagMode = OutputTagMode;

		// Set up the viewmodel to send to the Razor template
		var model = new TModel
		{
			InputHtml = await CreateInputCore(context, output),
			LabelHtml = await CreateLabel(context, output),
			ElementOptions = new TOptions()
		};

		SetupModelOptions(model.ElementOptions);
		AddCssClasses(model);

		return model;
	}

	/// <summary>
	/// Runs any additional processing on the model (e.g., processing additional properties for a child class of <see cref="MarkupModel{TOptions}"/>)
	/// </summary>
	/// <param name="model">The model to process</param>
	protected virtual Task ProcessModel(TModel model) => Task.CompletedTask;

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

		TagHelperContent labelChildContent = new DefaultTagHelperContent();
		var providedChildContent = await output.GetChildContentAsync();
		if (providedChildContent.IsEmptyOrWhiteSpace
		    && !string.IsNullOrEmpty(For.Metadata.DisplayName))
		{
			providedChildContent.AppendHtml(
				Utilities.GenerateLabelText(Options, For.Metadata.DisplayName));
		}

		if (Options.RenderInputInsideLabel)
		{
			var inputContent = await CreateInputCore(context, output);

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
		}
		else if (LabelReceivesChildContent)
		{
			labelChildContent = labelChildContent.AppendHtml(providedChildContent);
		}

		labelOutput.Content.SetHtmlContent(labelChildContent);

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
	protected Task<TagHelperOutput> CreateInputCore(TagHelperContext context, TagHelperOutput output)
	{
		var attributes = Utilities.GetInputAttributes(output.Attributes);
		return CreateInput(context, output, attributes);
	}

	/// <summary>
	/// Creates the &lt;input&gt; tag
	/// </summary>
	/// <param name="context">The <see cref="TagHelperContext"/></param>
	/// <param name="output">The <see cref="TagHelperOutput"/> of the root element</param>
	/// <param name="attributes">The attributes to pass to the &lt;input&gt;</param>
	/// <returns></returns>
	protected abstract Task<TagHelperOutput> CreateInput(
		TagHelperContext context,
		TagHelperOutput output,
		TagHelperAttributeList attributes);
#endregion

#region CSS generation
	/// <summary>
	/// Applies CSS classes to the &lt;input&gt; tag
	/// </summary>
	/// <param name="input">The <see cref="TagHelperOutput"/> for the &lt;input&gt; tag</param>
	protected virtual void ApplyCssClassesToInput(TagHelperOutput input)
	{
		AddClass(input, Options.InputClasses);
	}

	protected virtual void ApplyCssClassesToLabel(TagHelperOutput label)
	{
		AddClass(label, Options.LabelClasses);
	}

	/// <summary>
	/// Applies CSS classes directly to a <see cref="TagHelperOutput"/>
	/// </summary>
	/// <param name="output">The <see cref="TagHelperOutput"/> receiving the classes</param>
	/// <param name="classNames">A space-separated list of CSS classes to apply</param>
	protected virtual void AddClass(TagHelperOutput output, string? classNames)
	{
		if (string.IsNullOrWhiteSpace(classNames))
		{
			return;
		}

		foreach (var c in classNames.Split(' '))
		{
			if (string.IsNullOrEmpty(c))
			{
				continue;
			}

			output.AddClass(c, HtmlEncoder.Default);
		}
	}

	/// <summary>
	/// Adds CSS classes to the <see cref="MarkupModel{TOptions}"/> based on the provided options
	/// </summary>
	/// <param name="model">The <see cref="MarkupModel{TOptions}"/> to add classes to</param>
	protected virtual void AddCssClasses(MarkupModel<TOptions> model)
	{
		model.ElementOptions.ComponentWrapperClasses = Options.ComponentWrapperClasses;
		model.ElementOptions.InputBlockWrapperClasses = Options.InputBlockWrapperClasses;
		model.ElementOptions.InputWrapperClasses = Options.InputWrapperClasses;
		model.ElementOptions.LabelWrapperClasses = Options.LabelWrapperClasses;
	}
#endregion
}