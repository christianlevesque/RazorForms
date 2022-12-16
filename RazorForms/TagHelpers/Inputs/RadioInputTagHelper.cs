// using System.Linq;
// using Microsoft.AspNetCore.Mvc.ViewFeatures;
// using Microsoft.AspNetCore.Razor.TagHelpers;
// using Microsoft.Extensions.Logging;
// using RazorForms.Generators.Elements;
// using RazorForms.Generators.Inputs;
// using RazorForms.Options;
// using RazorForms.Options.Inputs;
//
// namespace RazorForms.TagHelpers.Inputs;
//
// public class RadioInputTagHelper : CheckRadioTagHelperBase
// {
// 	/// <inheritdoc />
// 	public RadioInputTagHelper(IHtmlGenerator generator,
// 	                           IRadioInputOptions options,
// 	                           IInputBlockWrapperGenerator wrapperGenerator,
// 	                           ILabelGenerator labelGenerator,
// 	                           IRadioInputGenerator inputGenerator) : base(generator,
// 	                                                                       options,
// 	                                                                       wrapperGenerator,
// 	                                                                       labelGenerator,
// 	                                                                       inputGenerator)
// 	{
// 	}
//
// 	/// <inheritdoc />
// 	protected override void AddCheckedAttributeIfAppropriate(TagHelperAttributeList attributes)
// 	{
// 		var currentValue = attributes.FirstOrDefault(a => a.Name == "value");
// 		if (currentValue == null)
// 		{
// 			return;
// 		}
//
// 		var setValue = ViewContext?.ViewData.Eval(For!.Name);
// 		if (setValue == null)
// 		{
// 			return;
// 		}
//
// 		if (currentValue.Value.ToString() == setValue.ToString())
// 		{
// 			attributes.Add(HtmlCheckedAttributeName, null);
// 		}
// 	}
// }