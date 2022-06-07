using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;
using RazorForms.Options;

namespace RazorForms.Templates;

public abstract class TemplateBase<TModel> : RazorPage<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	protected IFormComponentOptions InputOptions => Model.Options;
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

	protected string GenerateClasses(string? defaultClass = null,
	                                 string? validStateClass = null,
	                                 string? invalidStateClass = null,
	                                 bool checkValidationState = true,
	                                 string? startingValue = null)
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