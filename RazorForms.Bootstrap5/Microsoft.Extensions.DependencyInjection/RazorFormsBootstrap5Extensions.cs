using System;
using RazorForms;
using RazorForms.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RazorFormsBootstrap5Extensions
{
	/// <summary>
	/// Adds RazorForms support, configured to use basic Bootstrap 5 settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include basic Bootstrap 5 support. This will probably serve as a starting place for your own app, but you'll probably want to add customized options in all but the most basic of scenarios.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap5<T>(this IServiceCollection self)
		where T : RazorFormsOptions, new() => self.UseRazorForms<T>(ApplyBootstrapDefaults);

	/// <summary>
	/// Adds RazorForms support with configurable Bootstrap 5 settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Bootstrap 5 support.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bootstrapv5 options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap5<T>(this IServiceCollection self, Action<T> action)
		where T : RazorFormsOptions, new()
	{
		var bootstrap = new T();
		action(bootstrap);
		ApplyBootstrapDefaults(bootstrap);

		return self.UseRazorForms(bootstrap);
	}

	/// <summary>
	/// Add RazorForms support with configurable Bootstrap 5 settings, along with added configuration to set up Bootstrap floating form labels
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bootstrapv5 options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap5FloatingLabels<T>(this IServiceCollection self,
		Action<T> action)
		where T : RazorFormsOptions, new()
	{
		var bootstrap = new T();
		action(bootstrap);
		ApplyBootstrapDefaults(bootstrap);

		// Inputs
		bootstrap.TextInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("form-floating", bootstrap.TextInputOptions.InputBlockWrapperClasses);
		bootstrap.TextInputOptions.InputFirst = true;
		bootstrap.TextInputOptions.RemoveWrappers = true;

		// TextAreas
		bootstrap.TextAreaInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("form-floating", bootstrap.TextAreaInputOptions.InputBlockWrapperClasses);
		bootstrap.TextAreaInputOptions.InputFirst = true;
		bootstrap.TextAreaInputOptions.RemoveWrappers = true;

		// Selects
		bootstrap.SelectInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("form-floating", bootstrap.SelectInputOptions.InputBlockWrapperClasses);
		bootstrap.SelectInputOptions.InputFirst = true;
		bootstrap.SelectInputOptions.RemoveWrappers = true;

		return self.UseRazorForms(bootstrap);
	}

	public static void ApplyBootstrapDefaults<T>(T o)
		where T : RazorFormsOptions
	{
		// Text input
		o.TextInputOptions.LabelClasses = Utilities.MergeCssStrings("form-label", o.TextInputOptions.LabelClasses);
		o.TextInputOptions.LabelValidClasses = Utilities.MergeCssStrings("text-success", o.TextInputOptions.LabelValidClasses);
		o.TextInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("text-danger", o.TextInputOptions.LabelInvalidClasses);
		o.TextInputOptions.InputClasses = Utilities.MergeCssStrings("form-control", o.TextInputOptions.InputClasses);
		o.TextInputOptions.InputValidClasses = Utilities.MergeCssStrings("is-valid", o.TextInputOptions.InputValidClasses);
		o.TextInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("is-invalid", o.TextInputOptions.InputInvalidClasses);
		o.TextInputOptions.ErrorWrapperClasses = Utilities.MergeCssStrings("text-danger list-unstyled", o.TextInputOptions.ErrorWrapperClasses);
		o.TextInputOptions.RemoveWrappers = true;

		// Text area input
		o.TextAreaInputOptions.LabelClasses = Utilities.MergeCssStrings("form-label", o.TextAreaInputOptions.LabelClasses);
		o.TextAreaInputOptions.LabelValidClasses = Utilities.MergeCssStrings("text-success", o.TextAreaInputOptions.LabelValidClasses);
		o.TextAreaInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("text-danger", o.TextAreaInputOptions.LabelInvalidClasses);
		o.TextAreaInputOptions.InputClasses = Utilities.MergeCssStrings("form-control", o.TextAreaInputOptions.InputClasses);
		o.TextAreaInputOptions.InputValidClasses = Utilities.MergeCssStrings("is-valid", o.TextAreaInputOptions.InputValidClasses);
		o.TextAreaInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("is-invalid", o.TextAreaInputOptions.InputInvalidClasses);
		o.TextAreaInputOptions.ErrorWrapperClasses = Utilities.MergeCssStrings("text-danger list-unstyled", o.TextAreaInputOptions.ErrorWrapperClasses);
		o.TextAreaInputOptions.RemoveWrappers = true;

		// Select input
		o.SelectInputOptions.LabelClasses = Utilities.MergeCssStrings("form-label", o.SelectInputOptions.LabelClasses);
		o.SelectInputOptions.LabelValidClasses = Utilities.MergeCssStrings("text-success", o.SelectInputOptions.LabelValidClasses);
		o.SelectInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("text-danger", o.SelectInputOptions.LabelInvalidClasses);
		o.SelectInputOptions.InputClasses = Utilities.MergeCssStrings("form-control", o.SelectInputOptions.InputClasses);
		o.SelectInputOptions.InputValidClasses = Utilities.MergeCssStrings("is-valid", o.SelectInputOptions.InputValidClasses);
		o.SelectInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("is-invalid", o.SelectInputOptions.InputInvalidClasses);
		o.SelectInputOptions.ErrorWrapperClasses = Utilities.MergeCssStrings("text-danger list-unstyled", o.SelectInputOptions.ErrorWrapperClasses);
		o.SelectInputOptions.RemoveWrappers = true;

		// Check input
		o.CheckInputOptions.ComponentWrapperClasses = Utilities.MergeCssStrings("form-check", o.CheckInputOptions.ComponentWrapperClasses);
		o.CheckInputOptions.LabelClasses = Utilities.MergeCssStrings("form-check-label", o.CheckInputOptions.LabelClasses);
		o.CheckInputOptions.InputClasses = Utilities.MergeCssStrings("form-check-input", o.CheckInputOptions.InputClasses);
		o.CheckInputOptions.InputFirst = true;
		o.CheckInputOptions.RemoveWrappers = true;

		// Check input group
		o.CheckInputGroupOptions.LabelClasses = Utilities.MergeCssStrings("form-label", o.CheckInputGroupOptions.LabelClasses);
		o.CheckInputGroupOptions.LabelValidClasses = Utilities.MergeCssStrings("text-success", o.CheckInputGroupOptions.LabelValidClasses);
		o.CheckInputGroupOptions.LabelInvalidClasses = Utilities.MergeCssStrings("text-danger", o.CheckInputGroupOptions.LabelInvalidClasses);
		o.CheckInputGroupOptions.ErrorWrapperClasses = Utilities.MergeCssStrings("text-danger list-unstyled", o.CheckInputGroupOptions.ErrorWrapperClasses);

		// Radio input
		o.RadioInputOptions.ComponentWrapperClasses = Utilities.MergeCssStrings("form-check", o.RadioInputOptions.ComponentWrapperClasses);
		o.RadioInputOptions.LabelClasses = Utilities.MergeCssStrings("form-check-label", o.RadioInputOptions.LabelClasses);
		o.RadioInputOptions.InputClasses = Utilities.MergeCssStrings("form-check-input", o.RadioInputOptions.InputClasses);
		o.RadioInputOptions.InputFirst = true;
		o.RadioInputOptions.RemoveWrappers = true;

		// Radio input group
		o.RadioInputGroupOptions.LabelClasses = Utilities.MergeCssStrings("form-label", o.RadioInputGroupOptions.LabelClasses);
		o.RadioInputGroupOptions.LabelValidClasses = Utilities.MergeCssStrings("text-success", o.RadioInputGroupOptions.LabelValidClasses);
		o.RadioInputGroupOptions.LabelInvalidClasses = Utilities.MergeCssStrings("text-danger", o.RadioInputGroupOptions.LabelInvalidClasses);
		o.RadioInputGroupOptions.ErrorWrapperClasses = Utilities.MergeCssStrings("text-danger list-unstyled", o.RadioInputGroupOptions.ErrorWrapperClasses);
	}
}