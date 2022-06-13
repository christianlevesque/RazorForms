using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public interface IOutputGeneratorWithValidity<in TOptions> : IOutputGenerator<TOptions>
{
	void Init(TOptions options, bool isValid, bool isInvalid);
}