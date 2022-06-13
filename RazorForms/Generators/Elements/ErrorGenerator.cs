using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Elements;

public class ErrorGenerator : OutputGeneratorWithValidity<IFormComponentOptions>, IErrorGenerator
{
	private IList<string>? _errors;

	protected ModelExpression? For;
	protected ViewContext? ViewContext;

	public void Init(IFormComponentOptions options,
	                 bool isValid,
	                 bool isInvalid,
	                 ModelExpression modelExpression,
	                 ViewContext viewContext)
	{
		if (modelExpression == null)
		{
			throw new InvalidOperationException($"Cannot generate error list without a valid {typeof(ModelExpression)}");
		}

		if (viewContext == null)
		{
			throw new InvalidOperationException($"Cannot generate error list without a valid {typeof(ViewContext)}");
		}

		Init(options, isValid, isInvalid);
		For = modelExpression;
		ViewContext = viewContext;
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
	public override Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                            RazorFormsTagHelperBase helper,
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