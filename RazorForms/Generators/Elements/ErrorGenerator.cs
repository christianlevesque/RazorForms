using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.Generators.Elements;

public class ErrorGenerator : OutputGeneratorBase<IFormComponentWithValidationOptions>, IErrorGenerator
{
	private IList<string>? _errors;

	protected ModelExpression? For;
	protected ViewContext? ViewContext;
	protected bool IsValid;
	protected bool IsInvalid;

	public void Init(IFormComponentWithValidationOptions options,
	                 bool isValid,
	                 bool isInvalid,
	                 ModelExpression? modelExpression,
	                 ViewContext? viewContext)
	{
		if (IsInitialized)
		{
			return;
		}

		For = modelExpression ?? throw new InvalidOperationException($"Cannot generate error list without a valid {typeof(ModelExpression)}");

		ViewContext = viewContext ?? throw new InvalidOperationException($"Cannot generate error list without a valid {typeof(ViewContext)}");

		IsValid = isValid;
		IsInvalid = isInvalid;

		Init(options);
	}

	protected IList<string> Errors
	{
		get
		{
			if (_errors != null)
			{
				return _errors;
			}

			var empty = new List<string>();
			if (!ViewContext!.ViewData.ModelState.TryGetValue(For!.Name, out var errors))
			{
				_errors = empty;
			}
			else
			{
				_errors = errors?.Errors.Select(e => e.ErrorMessage).ToList() ?? empty;
			}

			return _errors;
		}
	}

	/// <inheritdoc />
	public Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                            TagHelperAttributeList? attributes = null,
	                                            TagHelperContent? childContent = null)
	{
		ThrowIfNotInitialized();

		var output = new TagHelperOutput(tagName: Errors.Any() ? "ul" : "",
		                                 attributes: new TagHelperAttributeList(), 
		                                 getChildContentAsync: DefaultTagHelperContent);

		foreach (var e in Errors)
		{
			output.Content.AppendHtml($"<li class=\"{Options.ErrorClasses}\">{e}</li>");
		}

		ApplyWrapperClasses(output);
		return Task.FromResult(output);
	}

	/// <inheritdoc />
	protected override void ApplyWrapperClasses(TagHelperOutput output)
	{
		AddClass(output, Options.ErrorWrapperClasses);
	}
}