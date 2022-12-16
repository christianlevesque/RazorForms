using System;
using RazorForms;
using RazorForms.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RazorFormsMaterializeExtensions
{
	/// <summary>
	/// Adds RazorForms support, configured to use basic Materialize settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include basic Materialize support. This will probably serve as a starting place for your own app, but you'll probably want to add customized options in all but the most basic of scenarios.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithMaterialize(this IServiceCollection self) => self.UseRazorForms(_materializeDefaults);

	/// <summary>
	/// Adds RazorForms support with configurable Materialize settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Materialize support.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Materialize options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithMaterialize(this IServiceCollection self, Action<RazorFormsOptions> action)
	{
		action(_materializeDefaults);
		return self.UseRazorForms(_materializeDefaults);
	}

	private static RazorFormsOptions _materializeDefaults = new()
	{
		InputOptions = new FormComponentWithValidationOptions
		{
			InputBlockWrapperClasses = "input-field",
			InputValidClasses = "valid",
			InputErrorClasses = "invalid",
			LabelValidClasses = "green-text",
			LabelErrorClasses = "red-text",
			RemoveWrappers = true,
			InputFirst = true,
			ErrorClasses = "helper-text red-text"
		},
		CheckInputOptions = new FormComponentOptions
		{
			RemoveWrappers = true,
			InputFirst = true,
			RenderInputInsideLabel = true,
			LabelTextHtmlWrapper = "span"
		},
		CheckInputGroupOptions = new FormComponentWithValidationOptions
		{
			LabelValidClasses = "green-text",
			LabelErrorClasses = "red-text",
			ErrorClasses = "helper-text red-text"
		},
		RadioInputOptions = new FormComponentOptions
		{
			RemoveWrappers = true,
			InputFirst = true,
			RenderInputInsideLabel = true,
			LabelTextHtmlWrapper = "span"
		},
		RadioInputGroupOptions = new FormComponentWithValidationOptions
		{
			LabelValidClasses = "green-text",
			LabelErrorClasses = "red-text",
			ErrorClasses = "helper-text red-text"
		},
		TextAreaOptions = new FormComponentWithValidationOptions
		{
			InputBlockWrapperClasses = "input-field",
			InputClasses = "materialize-textarea",
			InputValidClasses = "valid",
			InputErrorClasses = "invalid",
			LabelValidClasses = "green-text",
			LabelErrorClasses = "red-text",
			RemoveWrappers = true,
			InputFirst = true,
			ErrorClasses = "helper-text red-text"
		},
		SelectOptions = new FormComponentWithValidationOptions
		{
			InputBlockWrapperClasses = "input-field",
			InputValidClasses = "valid",
			InputErrorClasses = "invalid",
			LabelValidClasses = "green-text",
			LabelErrorClasses = "red-text",
			RemoveWrappers = true,
			InputFirst = true,
			ErrorClasses = "helper-text red-text"
		}
	};
}