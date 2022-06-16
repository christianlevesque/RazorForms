using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.TagHelpers;

public class RazorFormsTagHelperBase : TagHelper
{
	protected const string HtmlIdAttributeName = "id";
	protected const string HtmlForAttributeName = "for";
	protected const string HtmlCheckedAttributeName = "checked";
	protected const string ForAttributeName = "asp-for";

	public readonly IHtmlGenerator Generator;

    [HtmlAttributeNotBound]
    [ViewContext]
	public ViewContext? ViewContext { get; set; }

	[HtmlAttributeName(ForAttributeName)]
	public ModelExpression? For { get; set; }

	/// <inheritdoc />
	protected RazorFormsTagHelperBase(IHtmlGenerator generator)
	{
		Generator = generator;
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

	protected void ThrowIfForNull()
	{
		if (For == null)
		{
			throw new InvalidOperationException("Tag is missing required 'asp-for' attribute");
		}
	}
}