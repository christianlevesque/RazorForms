using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Models;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

/// <summary>
/// Adds common functionality for use among all validity-aware RazorForms tag helpers
/// </summary>
public abstract class ValidityAwareTagHelperBase<TOptions> : TagHelperBase<ValidityAwareMarkupModel, TOptions>
	where TOptions : ValidityAwareFormComponentOptions, new()
{
	protected ValidityAwareTagHelperBase(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		TOptions options) 
		: base(
			htmlGenerator,
			htmlHelper,
			options)
	{
	}

#region Validation
	private bool? _isValid;
	private bool? _isInvalid;
	private IList<string>? _errors;

	protected bool IsValid
	{
		get
		{
			if (_isValid.HasValue)
			{
				return _isValid.Value;
			}

			_isValid = ViewContext.ModelState.GetFieldValidationState(For.Name) == ModelValidationState.Valid;

			return _isValid.Value;
		}
	}

	protected bool IsInvalid
	{
		get
		{
			if (_isInvalid.HasValue)
			{
				return _isInvalid.Value;
			}

			_isInvalid = ViewContext.ModelState.GetFieldValidationState(For.Name) == ModelValidationState.Invalid;

			return _isInvalid.Value;
		}
	}

	protected IEnumerable<string> Errors
	{
		get
		{
			if (_errors != null)
			{
				return _errors;
			}

			_errors = !ViewContext.ViewData.ModelState.TryGetValue(For.Name, out var errors)
				? Array.Empty<string>() 
				: errors.Errors.Select(e => e.ErrorMessage).ToList();

			return _errors;
		}
	}
#endregion

#region Model generation and manipulation
	/// <inheritdoc/>
	protected override void ProcessModel(ValidityAwareMarkupModel model)
	{
		model.IsValid = IsValid;
		model.IsInvalid = IsInvalid;
		model.Errors = Errors;
	}

	protected override void SetupModelOptions(TOptions options)
	{
		base.SetupModelOptions(options);
		options.AlwaysRenderErrorContainer = Options.AlwaysRenderErrorContainer;
	}

#endregion

#region CSS generation
	protected override void AddCssClasses(
		TOptions options,
		TagHelperAttributeList attributeList)
	{
		var classAttribute = attributeList.FirstOrDefault(a => a.Name == "class");
		var componentWrapperClasses = Utilities.MergeCssStrings(classAttribute?.Value.ToString(), Options.ComponentWrapperClasses);

		options.ComponentWrapperClasses = CreateValidityAwareClasses(
			componentWrapperClasses,
			Options.ComponentWrapperValidClasses,
			Options.ComponentWrapperInvalidClasses);
		options.InputBlockWrapperClasses = CreateValidityAwareClasses(
			Options.InputBlockWrapperClasses,
			Options.InputBlockWrapperValidClasses,
			Options.InputBlockWrapperInvalidClasses);
		options.InputWrapperClasses = CreateValidityAwareClasses(
			Options.InputWrapperClasses,
			Options.InputWrapperValidClasses,
			Options.InputWrapperInvalidClasses);
		options.LabelWrapperClasses = CreateValidityAwareClasses(
			Options.LabelWrapperClasses,
			Options.LabelWrapperValidClasses,
			Options.LabelWrapperInvalidClasses);
		options.ErrorWrapperClasses = Options.ErrorWrapperClasses;
		options.ErrorClasses = Options.ErrorClasses;
	}

	/// <summary>
	/// Creates a final string of CSS classes based on the validation state of the current form element
	/// </summary>
	/// <param name="baseClasses">Classes that should be added regardless of validity</param>
	/// <param name="validClasses">Classes that should be added only if the form element is explicitly valid</param>
	/// <param name="invalidClasses">Classes that should be added only if the form element is explicitly invalid</param>
	/// <returns>The calculated CSS classes</returns>
	protected virtual string CreateValidityAwareClasses(
		string baseClasses,
		string validClasses,
		string invalidClasses)
	{
		var sb = new StringBuilder(baseClasses);

		if (IsValid)
		{
			sb.AppendWithLeadingSpace(validClasses);
		}
		else if (IsInvalid)
		{
			sb.AppendWithLeadingSpace(invalidClasses);
		}

		return sb.ToString();
	}

	/// <summary>
	/// Appends CSS classes based on whether the current form element is valid or not
	/// </summary>
	/// <param name="output">The <see cref="TagHelperOutput"/> to add classes to</param>
	/// <param name="baseClasses">Classes that should be added regardless of validity</param>
	/// <param name="validClasses">Classes that should be added only if the form element is explicitly valid</param>
	/// <param name="invalidClasses">Classes that should be added only if the form element is explicitly invalid</param>
	protected virtual void AddValidityAwareClasses(
		TagHelperOutput output,
		string baseClasses,
		string validClasses,
		string invalidClasses)
	{
		Utilities.AddClassesToOutput(output, baseClasses);

		if (IsValid)
		{
			Utilities.AddClassesToOutput(output, validClasses);
		}
		else if (IsInvalid)
		{
			Utilities.AddClassesToOutput(output, invalidClasses);
		}
	}

	/// <inheritdoc />
	protected override void ApplyCssClassesToInput(TagHelperOutput input)
	{
		AddValidityAwareClasses(
			input,
			Options.InputClasses,
			Options.InputValidClasses,
			Options.InputInvalidClasses);
	}

	/// <inheritdoc />
	protected override void ApplyCssClassesToLabel(TagHelperOutput label)
	{
		AddValidityAwareClasses(
			label,
			Options.LabelClasses,
			Options.LabelValidClasses,
			Options.LabelInvalidClasses);
	}
#endregion
}