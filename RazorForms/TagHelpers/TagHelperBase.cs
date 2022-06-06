using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.TagHelpers;

public class TagHelperBase : TagHelper, IAdditionalFormClasses
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
	public string? AdditionalComponentWrapperClasses { get; set; }
	public string? AdditionalInputBlockWrapperClasses { get; set; }
	public string? AdditionalLabelWrapperClasses { get; set; }
	public string? AdditionalLabelClasses { get; set; }
	public string? AdditionalInputWrapperClasses { get; set; }
	public string? AdditionalErrorWrapperClasses { get; set; }
	public string? AdditionalErrorClasses { get; set; }

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
			Attributes = output.Attributes,
			AdditionalComponentWrapperClasses = AdditionalComponentWrapperClasses,
			AdditionalInputBlockWrapperClasses = AdditionalInputBlockWrapperClasses,
			AdditionalLabelWrapperClasses = AdditionalLabelWrapperClasses,
			AdditionalLabelClasses = AdditionalLabelClasses,
			AdditionalInputWrapperClasses = AdditionalInputWrapperClasses,
			AdditionalErrorWrapperClasses = AdditionalErrorWrapperClasses,
			AdditionalErrorClasses = AdditionalErrorClasses
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