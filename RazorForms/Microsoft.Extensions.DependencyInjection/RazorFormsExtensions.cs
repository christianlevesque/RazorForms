using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
	/// <param name="o">The options to use when creating markup</param>
	/// <param name="types">An array of <see cref="Type"/> that the options should be registered as</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorForms<T>(this IServiceCollection self, T o, params Type[] types)
		where T : RazorFormsOptions, new()
	{
		// Set up types to add options as
		var typesList = new List<Type>(types)
		{
			typeof(RazorFormsOptions),
			o.GetType()
		};

		// Set up template paths
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

		// Add options to DI
		foreach (var t in typesList)
		{
			self.TryAdd(new ServiceDescriptor(t, o));
		}

		return self;
	}

	/// <summary>
	/// Adds RazorForms support with configurable settings
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default options</param>
	/// <param name="types">An array of <see cref="Type"/> that the options should be registered as</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorForms<T>(this IServiceCollection self, Action<T> action, params Type[] types)
		where T : RazorFormsOptions, new()
	{
		var options = new T();
		action(options);
		return self.UseRazorForms(options, types);
	}
}