using System;
using RazorForms;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds RazorForms support using the supplied <see cref="RazorFormsOptions"/> instance
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to supply your own <see cref="RazorFormsOptions"/> instance, such as when using a custom CSS solution, or when using a framework that isn't supported by RazorForms
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The options to use when creating markup</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorForms(this IServiceCollection self, RazorFormsOptions options) => self.AddSingleton(options);

	/// <summary>
	/// Adds RazorForms support, configured to use basic Bootstrap 5 settings
	/// </summary>
	/// <remarks>
	/// Use this overload when you want to include basic Bootstrap 5 support. This will probably serve as a starting place for your own app, but you'll probably want to add customized options in all but the most basic of scenarios.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorFormsWithBootstrap(this IServiceCollection self) => UseRazorForms(self, _bootstrapDefaults);

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
		return UseRazorForms(self, _bootstrapDefaults);
	}

	private static RazorFormsOptions _bootstrapDefaults = new()
	{
		LabelClasses = "form-label",
		LabelErrorClasses = "text-danger",
		LabelValidClasses = "text-success",
		InputClasses = "form-control",
		InputValidClasses = "is-valid",
		InputErrorClasses = "is-invalid",
		ErrorWrapperClasses = "text-danger list-unstyled"
	};
}