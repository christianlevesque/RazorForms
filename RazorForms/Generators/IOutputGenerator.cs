using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public interface IOutputGenerator
{
	Task<TagHelperOutput> GenerateOutput(TagHelperContext context, TextInputTagHelper helper, TagHelperAttributeList? attributes = null);
	string Render(TagHelperOutput output);
}