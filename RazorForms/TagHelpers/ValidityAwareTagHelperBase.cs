using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Models;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public abstract class ValidityAwareTagHelperBase : TagHelperBase<ValidityAwareMarkupModel, FormComponentWithValidationOptions>
{
	protected ValidityAwareTagHelperBase(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper,
		FormComponentWithValidationOptions options) 
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
	protected override Task ProcessModel(ValidityAwareMarkupModel model)
	{
		model.IsValid = IsValid;
		model.IsInvalid = IsInvalid;
		model.Errors = Errors;

		return Task.CompletedTask;
	}
#endregion

#region CSS generation
	protected override void AddCssClasses(
		MarkupModel<FormComponentWithValidationOptions> model)
	{
		base.AddCssClasses(model);

		model.ElementOptions.ErrorWrapperClasses = Options.ErrorWrapperClasses;
		model.ElementOptions.ErrorClasses = Options.ErrorClasses;
		model.ElementOptions.AlwaysShowErrorContainer = Options.AlwaysShowErrorContainer;
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
		AddClass(output, baseClasses);

		if (IsValid)
		{
			AddClass(output, validClasses);
		}
		else if (IsInvalid)
		{
			AddClass(output, invalidClasses);
		}
	}

	/// <inheritdoc />
	protected override void ApplyCssClassesToInput(TagHelperOutput input)
	{
		AddValidityAwareClasses(
			input,
			Options.InputClasses,
			Options.InputValidClasses,
			Options.InputErrorClasses);
	}

	/// <inheritdoc />
	protected override void ApplyCssClassesToLabel(TagHelperOutput label)
	{
		AddValidityAwareClasses(
			label,
			Options.LabelClasses,
			Options.LabelValidClasses,
			Options.LabelErrorClasses);
	}
#endregion
}