using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.TagHelpers;

public abstract class RazorFormsTagHelperBase : TagHelper
{
	private readonly IHtmlGenerator _generator;
    private readonly IHtmlHelper _helper;

    protected Func<bool, HtmlEncoder, Task<TagHelperContent>> DefaultTagHelperContent =
	    (a, b) => Task.Factory.StartNew<TagHelperContent>(() => new DefaultTagHelperContent());

	[HtmlAttributeName("asp-for")]
	public ModelExpression For { get; set; } = default!;

	protected bool LabelReceivesChildContent { get; set; }

	[HtmlAttributeNotBound]
	[ViewContext]
	public ViewContext ViewContext { get; set; } = default!;

	/// <inheritdoc />
	protected RazorFormsTagHelperBase(IHtmlGenerator generator, IHtmlHelper helper)
	{
		_generator = generator;
		_helper = helper;
	}

	protected async Task<T> GenerateHtmlModel<T>(TagHelperContext context, TagHelperOutput output)
		where T : MarkupModel, new()
	{
		output.TagName = "div";
		output.TagMode = TagMode.StartTagAndEndTag;
		(_helper as IViewContextAware)!.Contextualize(ViewContext);

		var labelOutput = await CreateLabel(context, output);

		var model = new T
		{
			InputHtml = await CreateInput(context, output),
			LabelHtml = labelOutput
		};

		return model;
	}

	protected abstract Task<TagHelperOutput> CreateInput(
		TagHelperContext context,
		TagHelperOutput output,
		TagHelperAttributeList attributes);

	protected virtual async Task<TagHelperOutput> CreateLabel(
		TagHelperContext context,
		TagHelperOutput output,
		TagHelperAttributeList attributes)
	{
		var labelHelper = new LabelTagHelper(_generator)
		{
			ViewContext = ViewContext,
			For = For
		};

		var labelOutput = new TagHelperOutput(
			"label",
			new TagHelperAttributeList(),
			DefaultTagHelperContent);

		await labelHelper.ProcessAsync(context, labelOutput);
		return output;
	}

	protected static TagHelperAttributeList ProcessAttributes(TagHelperOutput output)
	{
		var attributes = output.Attributes.Where(a => a.Name != "class");
		var value = new TagHelperAttributeList(attributes);
		var classAttribute = output.Attributes.FirstOrDefault(a => a.Name == "class");
		output.Attributes.Clear();
		if (classAttribute != null)
		{
			output.Attributes.Add(classAttribute);
		}

		return value;
	}
}