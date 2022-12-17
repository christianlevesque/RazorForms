using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers.Inputs;

public class CheckInputTagHelper : CheckRadioTagHelperBase
{
	public CheckInputTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		RazorFormsOptions options)
		: base(
			htmlGenerator,
			htmlHelper,
			options.CheckInputOptions)
	{
		LabelReceivesChildContent = true;
		Type = "checkbox";
	}

	protected override TagHelper CreateInput(TagHelperAttributeList attributes)
	{
		return new InputTagHelper(HtmlGenerator)
		{
			ViewContext = ViewContext,
			For = For
		};
	}

	/// <inheritdoc/>
	protected override void AddCheckedAttribute(TagHelperAttributeList attributes)
	{
		var currentValue = attributes.FirstOrDefault(a => a.Name == "value");
		if (currentValue == null)
		{
			return;
		}

		var setValues = ViewContext.ViewData.Eval(For.Name);
		if (setValues == null)
		{
			return;
		}

		if (!setValues.GetType().IsGenericType
		    || setValues.GetType().GetGenericTypeDefinition() != typeof(List<>))
		{
			return;
		}

		IList usableValues;
		try
		{
			usableValues = (IList) setValues;
		}
		catch (Exception)
		{
			return;
		}

		var cv = currentValue.Value.ToString();

		foreach (var i in usableValues)
		{
			if (i?.ToString() == cv)
			{
				attributes.Add(HtmlCheckedAttributeName, null);
				return;
			}
		}
	}
}