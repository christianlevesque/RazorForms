using System;
using RazorForms.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RazorFormsExtensions
{
	public const string TemplateBasePath = "~/RazorFormsTemplates";
	public const string ValidityAwareContentPartial = $"{TemplateBasePath}/Partials/ValidityAwareContent.cshtml";
	public const string ContentPartial = $"{TemplateBasePath}/Partials/Content.cshtml";

	/// <summary>
	/// Adds RazorForms support using the supplied <see cref="RazorFormsOptions"/> instance
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The options to use when creating markup</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorForms(this IServiceCollection self, RazorFormsOptions o)
	{
		if (string.IsNullOrWhiteSpace(o.TextInputOptions.TemplatePath))
		{
			o.TextInputOptions.TemplatePath = ValidityAwareContentPartial;
		}

		if (string.IsNullOrWhiteSpace(o.TextAreaInputOptions.TemplatePath))
		{
			o.TextAreaInputOptions.TemplatePath = ValidityAwareContentPartial;
		}

		if (string.IsNullOrWhiteSpace(o.SelectInputOptions.TemplatePath))
		{
			o.SelectInputOptions.TemplatePath = ValidityAwareContentPartial;
		}

		if (string.IsNullOrWhiteSpace(o.CheckInputGroupOptions.TemplatePath))
		{
			o.CheckInputGroupOptions.TemplatePath = ValidityAwareContentPartial;
		}

		if (string.IsNullOrWhiteSpace(o.RadioInputGroupOptions.TemplatePath))
		{
			o.RadioInputGroupOptions.TemplatePath = ValidityAwareContentPartial;
		}

		if (string.IsNullOrWhiteSpace(o.CheckInputOptions.TemplatePath))
		{
			o.CheckInputOptions.TemplatePath = ContentPartial;
		}

		if (string.IsNullOrWhiteSpace(o.RadioInputOptions.TemplatePath))
		{
			o.RadioInputOptions.TemplatePath = ContentPartial;
		}

		return self.AddSingleton(o);
	}

	/// <summary>
	/// Adds RazorForms support with configurable settings
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorForms(this IServiceCollection self, Action<RazorFormsOptions> action)
	{
		var options = new RazorFormsOptions();
		action(options);
		return self.UseRazorForms(options);
	}
}