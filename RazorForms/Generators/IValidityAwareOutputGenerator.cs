using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public interface IValidityAwareOutputGenerator<in TOptions> : IOutputGeneratorBase<TOptions>
{
	void Init(TOptions options, bool isValid, bool isInvalid);
	Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                     RazorFormsTagHelperBase helper,
	                                     TagHelperAttributeList? attributes = null,
	                                     TagHelperContent? childContent = null);
}