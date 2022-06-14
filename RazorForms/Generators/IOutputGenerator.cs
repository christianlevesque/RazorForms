using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.Generators;

public interface IOutputGenerator<in TOptions> : IOutputGeneratorBase<TOptions>
{
	Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                     TagHelperAttributeList? attributes = null,
	                                     TagHelperContent? childContent = null);
}