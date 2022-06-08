using System.Linq;
using System.Text;
using RazorForms.Options;

namespace RazorForms.Templates;

public abstract class GeneralInputBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput<IFormComponentOptions>
{
	protected IFormComponentOptions InputOptions => Model.Options;

	protected void GenerateAdditionalAttributes(StringBuilder sb)
	{
		// aria-describedby
		var describedBy = string.Empty;
		if (Model.Errors.Any())
		{
			describedBy = GenerateErrorId(Model.MvcName);
		}

		// TODO: add input hint and additional aria-describedby ID to point to hint

		if (!string.IsNullOrEmpty(describedBy))
		{
			sb.AppendWithLeadingSpace($"aria-describedby=\"{describedBy}\"");
		}
	}

	/// <summary>
	/// Generates an HTML ID value for the error container
	/// </summary>
	/// <param name="input"></param>
	/// <returns>the HTML ID value</returns>
	protected static string GenerateErrorId(string input) => $"{input}Errors";

	/// <summary>
	/// Generates a string representing the appropriate classes for an element based on its validation state
	/// </summary>
	/// <param name="defaultClass">The default class(es) to apply to the element. These are always applied</param>
	/// <param name="validStateClass">The class(es) to apply to the element when the validation state is explicitly valid</param>
	/// <param name="invalidStateClass">The class(es) to apply to the element when the validation state is explicitly invalid</param>
	/// <param name="checkValidationState">Whether or not to apply validation state-based classes</param>
	/// <param name="startingValue">Additional classes to apply to the element. These are always applied</param>
	/// <returns>a string containing the space-separated classes applied, or an empty string if no classes were applied</returns>
	protected string GenerateClasses(string? defaultClass = null,
	                                 string? validStateClass = null,
	                                 string? invalidStateClass = null,
	                                 bool checkValidationState = true,
	                                 string? startingValue = null)
	{
		var classes = new StringBuilder(startingValue);
		classes.AppendWithLeadingSpace(defaultClass);

		if (checkValidationState)
		{
			if (Model.Errors.Any())
			{
				classes.AppendWithLeadingSpace(invalidStateClass);
			}
			else if (Model.IsValid)
			{
				classes.AppendWithLeadingSpace(validStateClass);
			}
		}

		return classes.ToString();
	}
}