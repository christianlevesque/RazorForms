using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Generators.Elements;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public abstract class ValidityUnawareTagHelperBase<TOutputGenerator> : RazorFormsTagHelperBase
{
	protected readonly IFormComponentOptions Options;
	protected readonly IInputBlockWrapperGenerator WrapperGenerator;
	protected readonly ILabelGenerator LabelGenerator;
	protected readonly TOutputGenerator InputGenerator;

	protected ValidityUnawareTagHelperBase(IHtmlGenerator generator,
	                                       IFormComponentOptions options,
	                                       IInputBlockWrapperGenerator wrapperGenerator,
	                                       ILabelGenerator labelGenerator,
	                                       TOutputGenerator inputGenerator) : base(generator)
	{
		Options = options;
		WrapperGenerator = wrapperGenerator;
		LabelGenerator = labelGenerator;
		InputGenerator = inputGenerator;
	}

	protected void AddCheckedAttributeIfAppropriate(TagHelperAttributeList attributes, object? values)
	{
		var currentValue = attributes.FirstOrDefault(a => a.Name == "value");
		if (currentValue == null)
		{
			return;
		}

		var setValues = ViewContext?.ViewData.Eval(For!.Name);
		if (setValues == null)
		{
			return;
		}

		if (!setValues.GetType().IsGenericType || setValues.GetType().GetGenericTypeDefinition() != typeof(List<>))
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