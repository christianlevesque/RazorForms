using System;
using RazorForms;
using RazorForms.Options.Elements;
using RazorForms.Options.Inputs;

namespace Microsoft.Extensions.DependencyInjection;

public static class BootstrapExtensions
{
	/// <summary>
	/// Adds RazorForms support, configured to use basic Bootstrap 5 settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include basic Bootstrap 5 support. This will probably serve as a starting place for your own app, but you'll probably want to add customized options in all but the most basic of scenarios.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap(this IServiceCollection self) => self.UseRazorForms(_bootstrapDefaults);

	/// <summary>
	/// Adds RazorForms support with configurable Bootstrap 5 settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Bootstrap 5 support.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bootstrapv5 options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap(this IServiceCollection self, Action<RazorFormsOptions> action)
	{
		action(_bootstrapDefaults);
		return self.UseRazorForms(_bootstrapDefaults);
	}

	private static RazorFormsOptions _bootstrapDefaults = new()
	{
		InputOptions = new InputOptions
		{
			LabelClasses = "form-label",
            LabelErrorClasses = "text-danger",
            LabelValidClasses = "text-success",
            InputClasses = "form-control",
            InputValidClasses = "is-valid",
            InputErrorClasses = "is-invalid",
            ErrorWrapperClasses = "text-danger list-unstyled"
		},
		CheckInputOptions = new CheckInputOptions
		{
			InputBlockWrapperClasses = "form-check",
			LabelClasses = "form-check-label",
			InputClasses = "form-check-input",
			InputFirst = true,
			RemoveWrappers = true
		},
		TextAreaOptions = new TextAreaOptions
		{
			LabelClasses = "form-label",
			LabelErrorClasses = "text-danger",
			LabelValidClasses = "text-success",
			InputClasses = "form-control",
			InputValidClasses = "is-valid",
			InputErrorClasses = "is-invalid",
			ErrorWrapperClasses = "text-danger list-unstyled"
		},
		SelectOptions = new SelectOptions
		{
			LabelClasses = "form-label",
			LabelErrorClasses = "text-danger",
			LabelValidClasses = "text-success",
			InputClasses = "form-select",
			InputValidClasses = "is-valid",
			InputErrorClasses = "is-invalid",
			ErrorWrapperClasses = "text-danger list-unstyled"
		},
		ButtonOptions = new ButtonOptions
		{
			SubmitButtonClasses = "btn btn-primary",
			ResetButtonClasses = "btn btn-outline-secondary",
			DefaultButtonClasses = "btn btn-secondary",
			RemoveWrappers = true
		}
	};
}