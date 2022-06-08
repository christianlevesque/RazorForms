using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.TagHelpers;

public abstract class InputHelperBase : TagHelperBase, IFormComponentOptions
{
	protected IInputOptions Options;

	/// <inheritdoc />
	protected InputHelperBase(IHtmlHelper html, IInputOptions options) : base(html)
	{
		Options = options;
	}

	/// <inheritdoc />
	public string? ComponentWrapperClasses { get; set; }

	/// <inheritdoc />
	public bool? RemoveWrappers { get; set; }

	/// <inheritdoc />
	public string? ComponentWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? ComponentWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? LabelClasses { get; set; }

	/// <inheritdoc />
	public string? LabelValidClasses { get; set; }

	/// <inheritdoc />
	public string? LabelErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputClasses { get; set; }

	/// <inheritdoc />
	public string? InputValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputErrorClasses { get; set; }

	/// <inheritdoc />
	public string? ErrorWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? ErrorClasses { get; set; }

	/// <inheritdoc />
	public bool? InputFirst { get; set; }

	[HtmlAttributeName("asp-for")]
	public ModelExpression For { get; set; } = default!;

	protected async Task<TModel> GetComponentModel<TModel>(TagHelperOutput output)
		where TModel : FormInput<IFormComponentOptions>, new()
	{
		return new TModel
		{
			ChildContent = await output.GetChildContentAsync(),
			DisplayName = For.ModelExplorer.Metadata.DisplayName ?? For.Name,
			MvcName = For.Name,
			Value = ViewContext.ViewData.Eval(For.Name),
			Errors = GetErrors(),
			IsValid = ViewContext.ModelState.GetFieldValidationState(For.Name) == ModelValidationState.Valid,
			Attributes = output.Attributes,
			Options = Options.Merge(this)
		};
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