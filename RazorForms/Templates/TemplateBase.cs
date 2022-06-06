using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.Options;

namespace RazorForms.Templates;

public abstract class TemplateBase<TModel> : RazorPage<TModel> where TModel : FormInput
{
	[RazorInject]
	public RazorFormsOptions Options { get; set; } = default!;

	protected string GenerateComponentWrapperClasses() => GenerateClasses(Options.ComponentWrapperClasses,
	                                                                      Options.ComponentWrapperValidClasses,
	                                                                      Options.ComponentWrapperErrorClasses,
	                                                                      startingValue: Model.AdditionalComponentWrapperClasses);

	protected string GenerateInputBlockWrapperClasses() => GenerateClasses(Options.InputBlockWrapperClasses,
	                                                                       Options.InputBlockWrapperValidClasses,
	                                                                       Options.InputBlockWrapperErrorClasses,
	                                                                       startingValue: Model.AdditionalInputBlockWrapperClasses);

	protected string GenerateLabelWrapperClasses() => GenerateClasses(Options.LabelWrapperClasses,
	                                                                  Options.LabelWrapperValidClasses,
	                                                                  Options.LabelWrapperErrorClasses,
	                                                                  startingValue: Model.AdditionalLabelWrapperClasses);

	protected string GenerateLabelClasses() => GenerateClasses(Options.LabelClasses,
	                                                           Options.LabelValidClasses,
	                                                           Options.LabelErrorClasses,
	                                                           startingValue: Model.AdditionalLabelClasses);

	protected string GenerateInputWrapperClasses() => GenerateClasses(Options.InputWrapperClasses,
	                                                                  Options.InputWrapperValidClasses,
	                                                                  Options.InputWrapperErrorClasses,
	                                                                  startingValue: Model.AdditionalInputWrapperClasses);

	protected string GenerateInputClasses()
	{
		var classAttribute = Model.Attributes.FirstOrDefault(a => a.Name == "class");
		var classValue = classAttribute?.Value.ToString() ?? string.Empty;

		return GenerateClasses(Options.InputClasses,
		                       Options.InputValidClasses,
		                       Options.InputErrorClasses,
		                       startingValue: classValue);
	}

	protected string GenerateErrorWrapperClasses() => GenerateClasses(Options.ErrorWrapperClasses,
	                                                                  string.Empty,
	                                                                  string.Empty,
	                                                                  false,
	                                                                  Model.AdditionalErrorWrapperClasses);

	protected string GenerateErrorClasses() => GenerateClasses(Options.ErrorClasses,
	                                                           string.Empty,
	                                                           string.Empty,
	                                                           false,
	                                                           Model.AdditionalErrorClasses);

	protected string GenerateAttributes()
	{
		var sb = new StringBuilder();
		foreach (var a in Model.Attributes)
		{
			if (a == null)
			{
				continue;
			}

			if (a.Value == null)
			{
				sb.Append($" {a.Name}");
			}
			else
			{
				sb.Append($" {a.Name}=\"{a.Value}\"");
			}
		}

		// aria-describedby
		var describedBy = string.Empty;
		if (Model.Errors.Any())
		{
			describedBy = GenerateErrorId(Model.MvcName);
		}

		// TODO: add input hint and additional aria-describedby ID to point to hint

		if (!string.IsNullOrEmpty(describedBy))
		{
			sb.Append($" aria-describedby=\"{describedBy}\"");
		}

		return sb.ToString();
	}

	protected static string GenerateErrorId(string input) => $"{input}Errors"; 

	private string GenerateClasses(string defaultClass, string validStateClass, string invalidStateClass, bool checkValidationState = true, string startingValue = "")
	{
		var classes = new StringBuilder(startingValue);
		classes.Append(' ');
		classes.Append(defaultClass);

		if (checkValidationState)
		{
			if (Model.Errors.Any())
			{
				classes.Append($" {invalidStateClass}");
			}
			else if (Model.IsValid)
			{
				classes.Append($" {validStateClass}");
			}
		}

		return classes.ToString();
	}
}