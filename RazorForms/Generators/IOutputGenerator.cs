using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public interface IOutputGenerator<in TOptions>
{
	void Init(TOptions options);
	Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                     RazorFormsTagHelperBase helper,
	                                     TagHelperAttributeList? attributes = null,
	                                     TagHelperContent? childContent = null);
	string Render(TagHelperOutput output);
}