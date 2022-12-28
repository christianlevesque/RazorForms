using System;
using RazorForms;
using RazorForms.Materialize.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RazorFormsMaterializeExtensions
{
	public const string FormattableContentPartial = "~/RazorFormsTemplates/Materialize/FormattableContent.cshtml";

	/// <summary>
	/// Adds RazorForms support, configured to use basic Materialize settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include basic Materialize support. This will probably serve as a starting place for your own app, but you'll probably want to add customized options in all but the most basic of scenarios.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithMaterialize<T>(this IServiceCollection self)
		where T : MaterializeOptions, new() => self.UseRazorForms<T>(ApplyMaterializeDefaults, typeof(MaterializeOptions));

	/// <summary>
	/// Adds RazorForms support with configurable Materialize settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include customizable Materialize support.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default Materialize options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithMaterialize<T>(this IServiceCollection self, Action<T> action)
		where T : MaterializeOptions, new()
	{
		var materialize = new T();
		action(materialize);
		ApplyMaterializeDefaults(materialize);

		return self.UseRazorForms(materialize, typeof(MaterializeOptions));
	}

	private static void ApplyMaterializeDefaults<T>(T o)
		where T : MaterializeOptions
	{
		// Text input
		o.TextInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("input-field", o.TextInputOptions.InputBlockWrapperClasses);
		o.TextInputOptions.InputValidClasses = Utilities.MergeCssStrings("valid", o.TextInputOptions.InputValidClasses);
		o.TextInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("invalid", o.TextInputOptions.InputInvalidClasses);
		o.TextInputOptions.LabelValidClasses = Utilities.MergeCssStrings("green-text", o.TextInputOptions.LabelValidClasses);
		o.TextInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("red-text", o.TextInputOptions.LabelInvalidClasses);
		o.TextInputOptions.ErrorClasses = Utilities.MergeCssStrings("helper-text red-text", o.TextInputOptions.ErrorClasses);
		o.TextInputOptions.RemoveWrappers = true;
		o.TextInputOptions.InputFirst = true;

		// Text area input
		o.TextAreaInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("input-field", o.TextAreaInputOptions.InputBlockWrapperClasses);
		o.TextAreaInputOptions.InputClasses = Utilities.MergeCssStrings("materialize-textarea", o.TextAreaInputOptions.InputClasses);
		o.TextAreaInputOptions.InputValidClasses = Utilities.MergeCssStrings("valid", o.TextAreaInputOptions.InputValidClasses);
		o.TextAreaInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("invalid", o.TextAreaInputOptions.InputInvalidClasses);
		o.TextAreaInputOptions.LabelValidClasses = Utilities.MergeCssStrings("green-text", o.TextAreaInputOptions.LabelValidClasses);
		o.TextAreaInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("red-text", o.TextAreaInputOptions.LabelInvalidClasses);
		o.TextAreaInputOptions.ErrorClasses = Utilities.MergeCssStrings("helper-text red-text", o.TextAreaInputOptions.ErrorClasses);
		o.TextAreaInputOptions.RemoveWrappers = true;
		o.TextAreaInputOptions.InputFirst = true;

		// Select input
		o.SelectInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("input-field", o.SelectInputOptions.InputBlockWrapperClasses);
		o.SelectInputOptions.InputValidClasses = Utilities.MergeCssStrings("valid", o.SelectInputOptions.InputValidClasses);
		o.SelectInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("invalid", o.SelectInputOptions.InputInvalidClasses);
		o.SelectInputOptions.LabelValidClasses = Utilities.MergeCssStrings("green-text", o.SelectInputOptions.LabelValidClasses);
		o.SelectInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("red-text", o.SelectInputOptions.LabelInvalidClasses);
		o.SelectInputOptions.ErrorClasses = Utilities.MergeCssStrings("helper-text red-text", o.SelectInputOptions.ErrorClasses);
		o.SelectInputOptions.RemoveWrappers = true;
		o.SelectInputOptions.InputFirst = true;

		// Check input
		o.CheckInputOptions.RemoveWrappers = true;
		o.CheckInputOptions.InputFirst = true;
		o.CheckInputOptions.RenderInputInsideLabel = true;
		o.CheckInputOptions.LabelTextHtmlWrapper =
			string.IsNullOrWhiteSpace(o.CheckInputOptions.LabelTextHtmlWrapper)
				? "span"
				: o.CheckInputOptions.LabelTextHtmlWrapper;

		// Check input group
		o.CheckInputGroupOptions.LabelValidClasses = Utilities.MergeCssStrings("green-text", o.CheckInputGroupOptions.LabelValidClasses);
		o.CheckInputGroupOptions.LabelInvalidClasses = Utilities.MergeCssStrings("red-text", o.CheckInputGroupOptions.LabelInvalidClasses);
		o.CheckInputGroupOptions.ErrorClasses = Utilities.MergeCssStrings("helper-text red-text", o.CheckInputGroupOptions.ErrorClasses);

		// Radio input
		o.RadioInputOptions.RemoveWrappers = true;
		o.RadioInputOptions.InputFirst = true;
		o.RadioInputOptions.RenderInputInsideLabel = true;
		o.RadioInputOptions.LabelTextHtmlWrapper =
			string.IsNullOrWhiteSpace(o.RadioInputOptions.LabelTextHtmlWrapper)
				? "span"
				: o.RadioInputOptions.LabelTextHtmlWrapper;

		// Radio input group
		o.RadioInputGroupOptions.LabelValidClasses = Utilities.MergeCssStrings("green-text", o.CheckInputGroupOptions.LabelValidClasses);
		o.RadioInputGroupOptions.LabelInvalidClasses = Utilities.MergeCssStrings("red-text", o.CheckInputGroupOptions.LabelInvalidClasses);
		o.RadioInputGroupOptions.ErrorClasses = Utilities.MergeCssStrings("helper-text red-text", o.CheckInputGroupOptions.ErrorClasses);

		// Custom Materialize inputs
		// DatePicker
		if (string.IsNullOrEmpty(o.DatePickerInputOptions.TemplatePath))
		{
			o.DatePickerInputOptions.TemplatePath = RazorFormsExtensions.ValidityAwareContentPartial;
		}

		if (string.IsNullOrEmpty(o.DatePickerInputOptions.Format))
		{
			o.DatePickerInputOptions.Format = "{0:MMM dd, yyyy}";
		}

		o.DatePickerInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("input-field", o.DatePickerInputOptions.InputBlockWrapperClasses);
		o.DatePickerInputOptions.InputClasses = Utilities.MergeCssStrings("datepicker", o.DatePickerInputOptions.InputClasses);
		o.DatePickerInputOptions.InputValidClasses = Utilities.MergeCssStrings("valid", o.DatePickerInputOptions.InputValidClasses);
		o.DatePickerInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("invalid", o.DatePickerInputOptions.InputInvalidClasses);
		o.DatePickerInputOptions.LabelValidClasses = Utilities.MergeCssStrings("green-text", o.DatePickerInputOptions.LabelValidClasses);
		o.DatePickerInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("red-text", o.DatePickerInputOptions.LabelInvalidClasses);
		o.DatePickerInputOptions.ErrorClasses = Utilities.MergeCssStrings("helper-text red-text", o.DatePickerInputOptions.ErrorClasses);
		o.DatePickerInputOptions.RemoveWrappers = true;
		o.DatePickerInputOptions.InputFirst = true;

		// TimePicker
		if (string.IsNullOrEmpty(o.TimePickerInputOptions.TemplatePath))
		{
			o.TimePickerInputOptions.TemplatePath = RazorFormsExtensions.ValidityAwareContentPartial;
		}

		if (string.IsNullOrEmpty(o.TimePickerInputOptions.Format))
		{
			o.TimePickerInputOptions.Format = "{0:t}";
		}

		o.TimePickerInputOptions.InputBlockWrapperClasses = Utilities.MergeCssStrings("input-field", o.TimePickerInputOptions.InputBlockWrapperClasses);
		o.TimePickerInputOptions.InputClasses = Utilities.MergeCssStrings("timepicker", o.TimePickerInputOptions.InputClasses);
		o.TimePickerInputOptions.InputValidClasses = Utilities.MergeCssStrings("valid", o.TimePickerInputOptions.InputValidClasses);
		o.TimePickerInputOptions.InputInvalidClasses = Utilities.MergeCssStrings("invalid", o.TimePickerInputOptions.InputInvalidClasses);
		o.TimePickerInputOptions.LabelValidClasses = Utilities.MergeCssStrings("green-text", o.TimePickerInputOptions.LabelValidClasses);
		o.TimePickerInputOptions.LabelInvalidClasses = Utilities.MergeCssStrings("red-text", o.TimePickerInputOptions.LabelInvalidClasses);
		o.TimePickerInputOptions.ErrorClasses = Utilities.MergeCssStrings("helper-text red-text", o.TimePickerInputOptions.ErrorClasses);
		o.TimePickerInputOptions.RemoveWrappers = true;
		o.TimePickerInputOptions.InputFirst = true;
	}
}