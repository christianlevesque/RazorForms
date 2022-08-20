using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Generators.Elements;
using RazorForms.Generators.Inputs;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public abstract class CheckRadioTagHelperBase : ValidityUnawareTagHelperBase<ICheckRadioInputGenerator>
{
	/// <inheritdoc />
	public CheckRadioTagHelperBase(IHtmlGenerator generator,
	                               IFormComponentOptions options,
	                               IInputBlockWrapperGenerator wrapperGenerator,
	                               ILabelGenerator labelGenerator,
	                               ICheckRadioInputGenerator inputGenerator) : base(generator,
	                                                                                options,
	                                                                                wrapperGenerator,
	                                                                                labelGenerator,
	                                                                                inputGenerator)
	{
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
		AddCheckedAttributeIfAppropriate(attributes);

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

	protected abstract void AddCheckedAttributeIfAppropriate(TagHelperAttributeList attributes);
}