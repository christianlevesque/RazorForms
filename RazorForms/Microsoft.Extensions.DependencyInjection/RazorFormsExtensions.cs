using System;
using RazorForms;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RazorFormsExtensions
{
	public const string TemplateBasePath = "RazorFormsTemplates";
	public const string ValidityAwareContentPartial = $"{TemplateBasePath}/Partials/ValidityAwareContent";
	public const string ContentPartial = $"{TemplateBasePath}/Partials/Content";

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
		var options = new RazorFormsOptions();
		action(options);
		return self.UseRazorForms(options);
	}
}