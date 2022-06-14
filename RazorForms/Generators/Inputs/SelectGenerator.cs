using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;
using RazorForms.TagHelpers.Inputs;

namespace RazorForms.Generators.Inputs;

public class SelectGenerator : ValidityAwareOutputGenerator<IFormComponentOptions>, ISelectGenerator
{
	/// <inheritdoc />
	public override Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     RazorFormsTagHelperBase helper,
	                                                     TagHelperAttributeList? attributes = null,
	                                                     TagHelperContent? childContent = null)
	{
		ThrowIfNotInitialized();

		if (helper is not SelectInputTagHelper localHelper)
		{
			throw new ArgumentException($"{nameof(helper)} must be an instance of {typeof(SelectInputTagHelper)}");
		}

		attributes ??= new TagHelperAttributeList();

		var selectHelper = new SelectTagHelper(localHelper.Generator)
		{
			ViewContext = localHelper.ViewContext,
			For = localHelper.For,
			Items = localHelper.Items
		};

		var output = new TagHelperOutput(tagName: "select",
		                                 attributes: attributes,
		                                 getChildContentAsync: DefaultTagHelperContent)
		{
			TagMode = TagMode.StartTagAndEndTag
		};
		if (childContent != null)
		{
			output.Content.SetHtmlContent(childContent.GetContent());
		}

		ApplyBaseClasses(output);
		selectHelper.Init(context);
		selectHelper.Process(context, output);

		var result = Options.RemoveWrappers ?? false
			             ? output
			             : GenerateWrapper(output);
		return Task.FromResult(result);
	}

	protected virtual void ApplyBaseClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output,
		             Options.InputClasses,
		             Options.InputValidClasses,
		             Options.InputErrorClasses);
	}

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output,
		             Options.InputWrapperClasses,
		             Options.InputWrapperValidClasses,
		             Options.InputWrapperErrorClasses);
	}
}