using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Buttons;

public abstract class ButtonGeneratorBase : OutputGeneratorBase<IButtonOptions>, IButtonGenerator
{
	protected ButtonType Type;

	public void Init(IButtonOptions options, ButtonType type)
	{
		if (IsInitialized)
		{
			return;
		}

		Type = type;
		Init(options);
	}

	/// <inheritdoc />
	public Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                            TagHelperAttributeList? attributes = null,
	                                            TagHelperContent? childContent = null)
	{
		ThrowIfNotInitialized();

		attributes ??= new TagHelperAttributeList();
		attributes.Add("type", Type.GetDescription());

		var output = new TagHelperOutput(tagName: "button",
		                                 attributes: attributes,
		                                 getChildContentAsync: DefaultTagHelperContent);

		output.Content.SetHtmlContent(childContent);

		ApplyBaseClasses(output);

		var finalOutput = Options.RemoveWrappers ?? false
			                  ? output
			                  : GenerateWrapper(output);

		return Task.FromResult(finalOutput);
	}

	protected abstract void ApplyBaseClasses(TagHelperOutput output);

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		ThrowIfNotInitialized();

		ApplyClasses(output, Options.ComponentWrapperClasses);
	}
}