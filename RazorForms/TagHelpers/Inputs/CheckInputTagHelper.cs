// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.Encodings.Web;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc.TagHelpers;
// using Microsoft.AspNetCore.Mvc.ViewFeatures;
// using Microsoft.AspNetCore.Razor.TagHelpers;
// using RazorForms.Generators.Elements;
// using RazorForms.Generators.Inputs;
// using RazorForms.Options.Inputs;
//
// namespace RazorForms.TagHelpers.Inputs;
//
// public class CheckInputTagHelper : CheckRadioTagHelperBase
// {
// 	/// <inheritdoc />
// 	public CheckInputTagHelper(IHtmlGenerator generator,
// 	                           ICheckInputOptions options,
// 	                           IInputBlockWrapperGenerator wrapperGenerator,
// 	                           ILabelGenerator labelGenerator,
// 	                           ICheckInputGenerator inputGenerator) : base(generator,
// 	                                                                       options,
// 	                                                                       wrapperGenerator,
// 	                                                                       labelGenerator,
// 	                                                                       inputGenerator)
// 	{
// 	}
//
// 	/// <inheritdoc/>
// 	protected override void AddCheckedAttributeIfAppropriate(TagHelperAttributeList attributes)
// 	{
// 		var currentValue = attributes.FirstOrDefault(a => a.Name == "value");
// 		if (currentValue == null)
// 		{
// 			return;
// 		}
//
// 		var setValues = ViewContext?.ViewData.Eval(For!.Name);
// 		if (setValues == null)
// 		{
// 			return;
// 		}
//
// 		if (!setValues.GetType().IsGenericType || setValues.GetType().GetGenericTypeDefinition() != typeof(List<>))
// 		{
// 			return;
// 		}
//
// 		IList usableValues;
// 		try
// 		{
// 			usableValues = (IList) setValues;
// 		}
// 		catch (Exception)
// 		{
// 			return;
// 		}
//
// 		var cv = currentValue.Value.ToString();
//
// 		foreach (var i in usableValues)
// 		{
// 			if (i?.ToString() == cv)
// 			{
// 				attributes.Add(HtmlCheckedAttributeName, null);
// 				return;
// 			}
// 		}
// 	}
// }