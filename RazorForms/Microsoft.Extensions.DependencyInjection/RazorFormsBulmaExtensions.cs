using System;
using RazorForms;
using RazorForms.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RazorFormsBulmaExtensions
{
	/// <summary>
	/// Adds RazorForms support, configured to use basic Bulma settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include basic Bulma support. This will probably serve as a starting place for your own app, but you'll probably want to add customized options in all but the most basic of scenarios.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBulma(this IServiceCollection self) =>
		self.UseRazorForms(_bulmaDefaults);

	/// <summary>
	/// Adds RazorForms support with configurable Bulma settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Bulma support.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bulma options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBulma(this IServiceCollection self,
		Action<RazorFormsOptions> action)
	{
		action(_bulmaDefaults);
		return self.UseRazorForms(_bulmaDefaults);
	}

	private static RazorFormsOptions _bulmaDefaults = new()
	{
		InputOptions = new FormComponentWithValidationOptions
		{
			ComponentWrapperClasses = "field",
			InputBlockWrapperClasses = "control",
			InputClasses = "input",
			InputValidClasses = "is-success",
			InputErrorClasses = "is-danger",
			LabelClasses = "label",
			LabelValidClasses = "has-text-success",
			LabelErrorClasses = "has-text-danger",
			ErrorClasses = "help is-danger",
			RemoveWrappers = true
		},
		CheckInputOptions = new FormComponentOptions
		{
			LabelClasses = "checkbox",
			InputClasses = "mr-2",
			InputFirst = true,
			RenderInputInsideLabel = true,
			RemoveWrappers = true
		},
		RadioInputOptions = new FormComponentOptions
		{
			LabelClasses = "radio",
			InputClasses = "mr-2",
			InputFirst = true,
			RenderInputInsideLabel = true,
			RemoveWrappers = true
		},
		CheckInputGroupOptions = new FormComponentWithValidationOptions
		{
			ComponentWrapperClasses = "field",
			LabelClasses = "label",
			LabelValidClasses = "has-text-success",
			LabelErrorClasses = "has-text-danger",
			ErrorClasses = "help is-danger"
		},
		RadioInputGroupOptions = new FormComponentWithValidationOptions
		{
			ComponentWrapperClasses = "field",
			LabelClasses = "label",
			LabelValidClasses = "has-text-success",
			LabelErrorClasses = "has-text-danger",
			ErrorClasses = "help is-danger"
		},
		TextAreaOptions = new FormComponentWithValidationOptions
		{
			ComponentWrapperClasses = "field",
			InputBlockWrapperClasses = "control",
			InputClasses = "textarea",
			InputValidClasses = "is-success",
			InputErrorClasses = "is-danger",
			LabelClasses = "label",
			LabelValidClasses = "has-text-success",
			LabelErrorClasses = "has-text-danger",
			ErrorClasses = "help is-danger",
			RemoveWrappers = true
		},
		SelectOptions = new FormComponentWithValidationOptions
		{
			ComponentWrapperClasses = "field",
			InputBlockWrapperClasses = "control",
			InputWrapperClasses = "select",
			InputWrapperValidClasses = "is-success",
			InputWrapperErrorClasses = "is-danger",
			LabelClasses = "label mb-2",
			LabelValidClasses = "has-text-success",
			LabelErrorClasses = "has-text-danger",
			ErrorClasses = "help is-danger"
		}
	};
}