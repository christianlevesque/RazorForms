using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;

namespace RazorForms.Templates;

public abstract class TemplateBase<TModel> : RazorPage<TModel>
	where TModel : FormInput
{
	protected string GenerateComponentWrapperClasses()
		=> GenerateClasses(Model.Options.ComponentWrapperClasses,
		                   Model.Options.ComponentWrapperValidClasses,
		                   Model.Options.ComponentWrapperErrorClasses);

	protected string GenerateInputBlockWrapperClasses()
		=> GenerateClasses(Model.Options.InputBlockWrapperClasses,
		                   Model.Options.InputBlockWrapperValidClasses,
		                   Model.Options.InputBlockWrapperErrorClasses);

	protected string GenerateLabelWrapperClasses()
		=> GenerateClasses(Model.Options.LabelWrapperClasses,
		                   Model.Options.LabelWrapperValidClasses,
		                   Model.Options.LabelWrapperErrorClasses);

	protected string GenerateLabelClasses()
		=> GenerateClasses(Model.Options.LabelClasses,
		                   Model.Options.LabelValidClasses,
		                   Model.Options.LabelErrorClasses);

	protected string GenerateInputWrapperClasses()
		=> GenerateClasses(Model.Options.InputWrapperClasses,
		                   Model.Options.InputWrapperValidClasses,
		                   Model.Options.InputWrapperErrorClasses);

	protected string GenerateInputClasses()
	{
		var classAttribute = Model.Attributes.FirstOrDefault(a => a.Name == "class");
		var classValue = classAttribute?.Value.ToString() ?? string.Empty;

		return GenerateClasses(Model.Options.InputClasses,
		                       Model.Options.InputValidClasses,
		                       Model.Options.InputErrorClasses,
		                       startingValue: classValue);
	}

	protected string GenerateErrorWrapperClasses()
		=> GenerateClasses(Model.Options.ErrorWrapperClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);

	protected string GenerateErrorClasses()
		=> GenerateClasses(Model.Options.ErrorClasses,
		                   string.Empty,
		                   string.Empty,
		                   false);

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

	private string GenerateClasses(string? defaultClass = null, string? validStateClass = null, string? invalidStateClass = null, bool checkValidationState = true, string? startingValue = null)
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