using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.TagHelpers;

public class TagHelperBase : TagHelper
{
	protected readonly IHtmlHelper Html;

	/// <inheritdoc />
	public TagHelperBase(IHtmlHelper html)
	{
		Html = html;
	}

	[HtmlAttributeNotBound]
	[ViewContext]
	public ViewContext ViewContext { get; set; } = default!;

	[HtmlAttributeName("asp-for")]
	public ModelExpression For { get; set; } = default!;

	public bool RemoveWrappers { get; set; }
	public bool InputFirst { get; set; }

	protected async Task<TModel> ProcessBase<TModel>(TagHelperOutput output) where TModel : FormInput, new()
	{
		(Html as IViewContextAware)!.Contextualize(ViewContext);
		output.TagName = string.Empty;
		var model = new TModel
		{
			ChildContent = await output.GetChildContentAsync(),
			DisplayName = For.ModelExplorer.Metadata.DisplayName ?? For.Name,
			MvcName = For.Name,
			Value = ViewContext.ViewData.Eval(For.Name),
			Errors = GetErrors(),
			IsValid = ViewContext.ModelState.GetFieldValidationState(For.Name) == ModelValidationState.Valid,
			RemoveWrappers = RemoveWrappers,
			InputFirst = InputFirst,
			Attributes = output.Attributes
		};
		return model;
	}

	protected IList<string> GetErrors()
	{
		var empty = new List<string>();
		if (!ViewContext.ViewData.ModelState.TryGetValue(For.Name, out var errors))
		{
			return empty;
		}

		return errors?.Errors.Select(e => e.ErrorMessage).ToList() ?? empty;
	}
}