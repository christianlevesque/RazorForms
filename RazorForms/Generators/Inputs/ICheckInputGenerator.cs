using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Inputs;

public interface ICheckInputGenerator : IOutputGenerator<IFormComponentOptions>
{
	Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                     RazorFormsTagHelperBase helper,
	                                     TagHelperAttributeList? attributes = null,
	                                     TagHelperContent? childContent = null);
}