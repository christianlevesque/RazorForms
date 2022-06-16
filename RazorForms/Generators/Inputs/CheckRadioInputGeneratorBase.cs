using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Inputs;

public abstract class CheckRadioInputGeneratorBase : OutputGeneratorBase<IFormComponentOptions>, ICheckRadioInputGenerator
{
	protected string? TypeAttributeValue;

	/// <inheritdoc />
	public Task<TagHelperOutput> GenerateOutput(TagHelperContext context, TagHelperAttributeList? attributes = null, TagHelperContent? childContent = null) => throw new NotImplementedException();

	/// <inheritdoc />
	public Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                            RazorFormsTagHelperBase helper,
	                                            TagHelperAttributeList? attributes = null,
	                                            TagHelperContent? childContent = null)
	{
		ThrowIfNotInitialized();

		attributes ??= new TagHelperAttributeList();
		attributes.Add("type", TypeAttributeValue);

		var inputHelper = new InputTagHelper(helper.Generator)
		{
			ViewContext = helper.ViewContext,
			For = helper.For
		};

		var output = new TagHelperOutput(tagName: "input",
		                                 attributes: new TagHelperAttributeList(attributes),
		                                 getChildContentAsync: DefaultTagHelperContent)
		{
			TagMode = TagMode.SelfClosing
		};

		ApplyBaseClasses(output);

		inputHelper.Process(context, output);

		var result = Options.RemoveWrappers ?? false
			             ? output
			             : GenerateWrapper(output);

		return Task.FromResult(result);
	}

	protected virtual void ApplyBaseClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output, Options.InputClasses);
	}

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output, Options.InputWrapperClasses);
	}
}