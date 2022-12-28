using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Models;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

/// <summary>
/// Adds common functionality for use between &lt;check-input&gt; and &lt;radio-input&gt;
/// </summary>
public abstract class CheckRadioTagHelperBase : TagHelperBase<MarkupModel, FormComponentOptions>
{
	protected const string HtmlIdAttributeName = "id";
	protected const string HtmlCheckedAttributeName = "checked";
	protected const string HtmlForAttributeName = "for";
	protected const string HtmlTypeAttributeName = "type";

	// This field will be boxed anyway, so might as well just convert it to a string up front
	private readonly string _id = Guid.NewGuid().ToString();

	/// <summary>
	/// The HTML type attribute to use for the input tag
	/// </summary>
	protected string Type { get; init; } = default!;

	public CheckRadioTagHelperBase(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		FormComponentOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options)
	{
		LabelReceivesChildContent = true;
	}

	protected override TagHelper CreateInputTagHelper()
	{
		return new InputTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For
		};
	}

	/// <inheritdoc/>
	protected override void AddCustomInputAttributes(TagHelperAttributeList attributes)
	{
		attributes.Add(HtmlIdAttributeName, _id);
		attributes.Add(HtmlTypeAttributeName, Type);
		AddCheckedAttribute(attributes);
	}

	/// <inheritdoc/>
	protected override void AddCustomLabelAttributes(TagHelperAttributeList attributes)
	{
		attributes.Add(HtmlForAttributeName, _id);
	}

	/// <summary>
	/// Adds the checked attribute to the attribute list for a checkbox or radio input if the value is selected in the model
	/// </summary>
	/// <param name="attributes">The attribute list to add the attribute to</param>
	protected abstract void AddCheckedAttribute(TagHelperAttributeList attributes);
}