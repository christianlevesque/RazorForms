using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Models;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public abstract class ValidityAwareTagHelperBase : TagHelperBase<ValidityAwareMarkupModel, FormComponentWithValidationOptions>
{
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

	protected override Task ProcessModel(ValidityAwareMarkupModel model)
	{
		model.IsValid = IsValid;
		model.IsInvalid = IsInvalid;
		model.Errors = Errors;

		return Task.CompletedTask;
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
}