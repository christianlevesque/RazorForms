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
	public static IServiceCollection UseRazorFormsWithBulma(this IServiceCollection self)
		=> self.UseRazorForms<RazorFormsOptions>(ApplyBulmaDefaults);

	/// <summary>
	/// Adds RazorForms support with configurable Bulma settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Bulma support.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Bulma options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBulma<T>(this IServiceCollection self, Action<T> action)
		where T : RazorFormsOptions, new()
	{
		var bulma = new T();
		action(bulma);
		ApplyBulmaDefaults(bulma);
		return self.UseRazorForms(bulma);
	}

	private static void ApplyBulmaDefaults<T>(T o)
		where T : RazorFormsOptions
	{
		// Text input
		o.TextInputOptions.ComponentWrapperClasses = Utilities.MergeCssStrings("field", o.TextInputOptions.ComponentWrapperClasses);
		o.TextInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("control", o.TextInputOptions.InputBlockWrapperClasses);
		o.TextInputOptions.InputClasses = Utilities.MergeCssStrings("input", o.TextInputOptions.InputClasses);
		o.TextInputOptions.InputValidClasses = Utilities.MergeCssStrings("is-success", o.TextInputOptions.InputValidClasses);
		o.TextInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("is-danger", o.TextInputOptions.InputInvalidClasses);
		o.TextInputOptions.LabelClasses = Utilities.MergeCssStrings("label", o.TextInputOptions.LabelClasses);
		o.TextInputOptions.LabelValidClasses = Utilities.MergeCssStrings("has-text-success", o.TextInputOptions.LabelValidClasses);
		o.TextInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("has-text-danger", o.TextInputOptions.LabelInvalidClasses);
		o.TextInputOptions.ErrorClasses = Utilities.MergeCssStrings("help is-danger", o.TextInputOptions.ErrorClasses);
		o.TextInputOptions.RemoveWrappers = true;

		// Text area input
		o.TextAreaInputOptions.ComponentWrapperClasses = Utilities.MergeCssStrings("field", o.TextAreaInputOptions.ComponentWrapperClasses);
		o.TextAreaInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("control", o.TextAreaInputOptions.InputBlockWrapperClasses);
		o.TextAreaInputOptions.InputClasses = Utilities.MergeCssStrings("textarea", o.TextAreaInputOptions.InputClasses);
		o.TextAreaInputOptions.InputValidClasses = Utilities.MergeCssStrings("is-success", o.TextAreaInputOptions.InputValidClasses);
		o.TextAreaInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("is-danger", o.TextAreaInputOptions.InputInvalidClasses);
		o.TextAreaInputOptions.LabelClasses = Utilities.MergeCssStrings("label", o.TextAreaInputOptions.LabelClasses);
		o.TextAreaInputOptions.LabelValidClasses = Utilities.MergeCssStrings("has-text-success", o.TextAreaInputOptions.LabelValidClasses);
		o.TextAreaInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("has-text-danger", o.TextAreaInputOptions.LabelInvalidClasses);
		o.TextAreaInputOptions.ErrorClasses = Utilities.MergeCssStrings("help is-danger", o.TextAreaInputOptions.ErrorClasses);
		o.TextAreaInputOptions.RemoveWrappers = true;

		// Select input
		o.TextInputOptions.ComponentWrapperClasses = Utilities.MergeCssStrings("field", o.TextInputOptions.ComponentWrapperClasses);
		o.TextInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("control", o.TextInputOptions.InputBlockWrapperClasses);
		o.TextInputOptions.InputClasses = Utilities.MergeCssStrings("input", o.TextInputOptions.InputClasses);
		o.TextInputOptions.InputValidClasses = Utilities.MergeCssStrings("is-success", o.TextInputOptions.InputValidClasses);
		o.TextInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("is-danger", o.TextInputOptions.InputInvalidClasses);
		o.TextInputOptions.LabelClasses = Utilities.MergeCssStrings("label mb-2", o.TextInputOptions.LabelClasses);
		o.TextInputOptions.LabelValidClasses = Utilities.MergeCssStrings("has-text-success", o.TextInputOptions.LabelValidClasses);
		o.TextInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("has-text-danger", o.TextInputOptions.LabelInvalidClasses);
		o.TextInputOptions.ErrorClasses = Utilities.MergeCssStrings("help is-danger", o.TextInputOptions.ErrorClasses);

		// Check input
		o.CheckInputOptions.LabelClasses = Utilities.MergeCssStrings("checkbox", o.CheckInputOptions.LabelClasses);
		o.CheckInputOptions.InputClasses = Utilities.MergeCssStrings("mr-2", o.CheckInputOptions.InputClasses);
		o.CheckInputOptions.RemoveWrappers = true;
		o.CheckInputOptions.InputFirst = true;
		o.CheckInputOptions.RenderInputInsideLabel = true;

		// Check input group
		o.CheckInputGroupOptions.ComponentWrapperClasses = Utilities.MergeCssStrings("field", o.CheckInputGroupOptions.ComponentWrapperClasses);
		o.CheckInputGroupOptions.LabelClasses = Utilities.MergeCssStrings("label", o.CheckInputGroupOptions.LabelClasses);
		o.CheckInputGroupOptions.LabelValidClasses = Utilities.MergeCssStrings("has-text-success", o.CheckInputGroupOptions.LabelValidClasses);
		o.CheckInputGroupOptions.LabelInvalidClasses = Utilities.MergeCssStrings("has-text-danger", o.CheckInputGroupOptions.LabelInvalidClasses);
		o.CheckInputGroupOptions.ErrorClasses = Utilities.MergeCssStrings("help is-danger", o.CheckInputGroupOptions.ErrorClasses);

		// Radio input
		o.CheckInputOptions.LabelClasses = Utilities.MergeCssStrings("radio", o.CheckInputOptions.LabelClasses);
		o.CheckInputOptions.InputClasses = Utilities.MergeCssStrings("mr-2", o.CheckInputOptions.InputClasses);
		o.CheckInputOptions.RemoveWrappers = true;
		o.CheckInputOptions.InputFirst = true;
		o.CheckInputOptions.RenderInputInsideLabel = true;

		// Radio input group
		o.RadioInputGroupOptions.ComponentWrapperClasses = Utilities.MergeCssStrings("field", o.RadioInputGroupOptions.ComponentWrapperClasses);
		o.RadioInputGroupOptions.LabelClasses = Utilities.MergeCssStrings("label", o.RadioInputGroupOptions.LabelClasses);
		o.RadioInputGroupOptions.LabelValidClasses = Utilities.MergeCssStrings("has-text-success", o.RadioInputGroupOptions.LabelValidClasses);
		o.RadioInputGroupOptions.LabelInvalidClasses = Utilities.MergeCssStrings("has-text-danger", o.RadioInputGroupOptions.LabelInvalidClasses);
		o.RadioInputGroupOptions.ErrorClasses = Utilities.MergeCssStrings("help is-danger", o.RadioInputGroupOptions.ErrorClasses);
	}
}