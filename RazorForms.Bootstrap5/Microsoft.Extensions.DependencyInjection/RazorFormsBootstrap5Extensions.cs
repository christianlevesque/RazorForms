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
	public static IServiceCollection UseRazorFormsWithBootstrap5(this IServiceCollection self)
		=> self.UseRazorForms<RazorFormsOptions>(ApplyBootstrapDefaults);

	/// <summary>
	/// Adds RazorForms support with configurable Bootstrap 5 settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Bootstrap 5 support with a custom subclass of <see cref="RazorFormsOptions"/>.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bootstrap5 options</param>
	/// <typeparam name="T">The type of the options class</typeparam>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap5<T>(
		this IServiceCollection self,
		Action<T> action)
		where T : RazorFormsOptions, new()
	{
		var bootstrap = new T();
		action(bootstrap);
		ApplyBootstrapDefaults(bootstrap);

		return self.UseRazorForms(bootstrap);
	}

	/// <summary>
	/// Adds RazorForms support with configurable Bootstrap 5 settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Bootstrap 5 support with the built-in <see cref="RazorFormsOptions"/>.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bootstrap5 options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap5(
		this IServiceCollection self,
		Action<RazorFormsOptions> action)
		=> UseRazorFormsWithBootstrap5<RazorFormsOptions>(self, action);

	/// <summary>
	/// Add RazorForms support with default Bootstrap 5 settings, along with added configuration to set up Bootstrap floating form labels
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include basic support for Bootstrap 5 with floating labels. This method doesn't allow any customization.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bootstrap5 options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap5FloatingLabels(this IServiceCollection self)
		=> self.UseRazorForms<RazorFormsOptions>(o =>
		{
			ApplyBootstrapDefaults(o);
			ApplyBootstrapFloatingLabel(o);
		});

	/// <summary>
	/// Add RazorForms support with configurable Bootstrap 5 settings, along with added configuration to set up Bootstrap floating form labels
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Bootstrap 5 support with a custom subclass of <see cref="RazorFormsOptions"/>.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bootstrap5 options</param>
	/// <typeparam name="T">The type of the options class</typeparam>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap5FloatingLabels<T>(
		this IServiceCollection self,
		Action<T> action)
		where T : RazorFormsOptions, new()
	{
		var bootstrap = new T();
		action(bootstrap);
		ApplyBootstrapDefaults(bootstrap);
		ApplyBootstrapFloatingLabel(bootstrap);

		return self.UseRazorForms(bootstrap);
	}

	/// <summary>
	/// Add RazorForms support with configurable Bootstrap 5 settings, along with added configuration to set up Bootstrap floating form labels
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Bootstrap 5 support with the built-in <see cref="RazorFormsOptions"/>.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bootstrap5 options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap5FloatingLabels(
		this IServiceCollection self,
		Action<RazorFormsOptions> action)
		=> UseRazorFormsWithBootstrap5FloatingLabels<RazorFormsOptions>(self, action);

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

	public static void ApplyBootstrapFloatingLabel<T>(T o)
		where T : RazorFormsOptions
	{
		// Inputs
		o.TextInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings(
			"form-floating",
			o.TextInputOptions.InputBlockWrapperClasses);
		o.TextInputOptions.InputFirst = true;
		o.TextInputOptions.RemoveWrappers = true;

		// TextAreas
		o.TextAreaInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings(
			"form-floating",
			o.TextAreaInputOptions.InputBlockWrapperClasses);
		o.TextAreaInputOptions.InputFirst = true;
		o.TextAreaInputOptions.RemoveWrappers = true;

		// Selects
		o.SelectInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings(
			"form-floating",
			o.SelectInputOptions.InputBlockWrapperClasses);
		o.SelectInputOptions.InputFirst = true;
		o.SelectInputOptions.RemoveWrappers = true;
	}
}