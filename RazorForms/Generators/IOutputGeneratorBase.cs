using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.Generators;

public interface IOutputGeneratorBase<in TOptions>
{
	void Init(TOptions options);
	string Render(TagHelperOutput output);
}