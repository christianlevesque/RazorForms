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
	public static IServiceCollection UseRazorForms(this IServiceCollection self, RazorFormsOptions options)
	{
		return self.AddSingleton(options);
	}

	/// <summary>
	/// Adds RazorForms support with configurable settings
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default options</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorForms(this IServiceCollection self, Action<RazorFormsOptions> action)
	{
		var options = new RazorFormsOptions
		{
			CheckInputOptions =
			{
				TemplatePath = ContentPartial
			},
			CheckInputGroupOptions =
			{
				TemplatePath = ValidityAwareContentPartial
			},
			RadioInputOptions =
			{
				TemplatePath = ContentPartial
			},
			RadioInputGroupOptions =
			{
				TemplatePath = ValidityAwareContentPartial
			},
			TextInputOptions =
			{
				TemplatePath = ValidityAwareContentPartial
			},
			TextAreaInputOptions = 
			{
				TemplatePath = ValidityAwareContentPartial
			},
			SelectInputOptions = 
			{
				TemplatePath = ValidityAwareContentPartial
			}
		};
		action(options);
		return self.UseRazorForms(options);
	}
}