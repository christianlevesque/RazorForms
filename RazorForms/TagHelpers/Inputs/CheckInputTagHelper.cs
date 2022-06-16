using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using RazorForms.Generators.Elements;
using RazorForms.Generators.Inputs;
using RazorForms.Options.Inputs;

namespace RazorForms.TagHelpers.Inputs;

public class CheckInputTagHelper : ValidityUnawareTagHelperBase<ICheckInputGenerator>
{
	private ILogger<CheckInputTagHelper> _logger;
	
	/// <inheritdoc />
	public CheckInputTagHelper(IHtmlGenerator generator,
	                           ICheckInputOptions options,
	                           IInputBlockWrapperGenerator wrapperGenerator,
	                           ILabelGenerator labelGenerator,
	                           ICheckInputGenerator inputGenerator, ILogger<CheckInputTagHelper> logger) : base(generator,
	                                                                       options,
	                                                                       wrapperGenerator,
	                                                                       labelGenerator,
	                                                                       inputGenerator)
	{
		_logger = logger;
	}

	/// <inheritdoc />
	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		ThrowIfForNull();

		output.TagName = "";

		// Creating list-aware input tags for checkboxes requires us to
		// do a little more to help Razor Pages than we're used to
		var htmlId = Guid.NewGuid().ToString();

		// Process the attributes and add the appropriate HTML id attribute
		var attributes = ProcessAttributes(output);
		attributes.Add(HtmlIdAttributeName, htmlId);

		// Add the checked attribute to the input if appropriate to do so
		var setValues = ViewContext?.ViewData.Eval(For!.Name);
		AddCheckedAttributeIfAppropriate(attributes, setValues);

		if (!string.IsNullOrEmpty(Options.ComponentWrapperClasses))
		{
			output.AddClass(Options.ComponentWrapperClasses, HtmlEncoder.Default);
		}

		// Generate wrapper
		WrapperGenerator.Init(Options);
		var wrapper = await WrapperGenerator.GenerateOutput(context, this);

		// Generate label
		LabelGenerator.Init(Options);
		var labelAttributes = new TagHelperAttributeList
		{
			{ HtmlForAttributeName, htmlId }
		};
		var label = await LabelGenerator.GenerateOutput(context, 
		                                                this,
		                                                labelAttributes,
		                                                await output.GetChildContentAsync());

		// Generate input
		InputGenerator.Init(Options);
		var input = await InputGenerator.GenerateOutput(context,
		                                                this,
		                                                attributes,
		                                                await output.GetChildContentAsync());

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

		output.Content.SetHtmlContent(WrapperGenerator.Render(wrapper));
	}
}