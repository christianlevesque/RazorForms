using System;
using RazorForms;
using RazorForms.Generators.Elements;
using RazorForms.Generators.Inputs;
using RazorForms.Options.Elements;
using RazorForms.Options.Inputs;

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
	public static IServiceCollection UseRazorForms(this IServiceCollection self, RazorFormsOptions options)
	{
		return self.ConfigureRazorFormsCore()
		           .ConfigureRazorFormsInputOptions(options.InputOptions)
		           .ConfigureRazorFormsTextAreaOptions(options.TextAreaOptions)
		           .ConfigureRazorFormsSelectOptions(options.SelectOptions)
		           .ConfigureRazorFormsButtonOptions(options.ButtonOptions);
	}

	public static IServiceCollection UseRazorForms(this IServiceCollection self, Action<RazorFormsOptions> action)
	{
		var options = new RazorFormsOptions();
		action(options);
		return self.UseRazorForms(options);
	}

	public static IServiceCollection ConfigureRazorFormsCore(this IServiceCollection self)
	{
		return self.AddTransient<ILabelGenerator, LabelGenerator>()
		           .AddTransient<IInputGenerator, InputGenerator>()
		           .AddTransient<ITextAreaGenerator, TextAreaGenerator>()
		           .AddTransient<IInputBlockWrapperGenerator, InputBlockWrapperGenerator>()
		           .AddTransient<ISelectGenerator, SelectGenerator>()
		           .AddTransient<IErrorGenerator, ErrorGenerator>()
		           .AddTransient<ISubmitButtonGenerator, SubmitButtonGenerator>()
		           .AddTransient<IResetButtonGenerator, ResetButtonGenerator>()
		           .AddTransient<IDefaultButtonGenerator, DefaultButtonGenerator>();
	}

	public static IServiceCollection ConfigureRazorFormsInputOptions(this IServiceCollection self, IInputOptions options) => self.AddSingleton(options);

	public static IServiceCollection ConfigureRazorFormsInputOptions(this IServiceCollection self, Action<IInputOptions> action)
	{
		var options = new InputOptions();
		action(options);
		return self.ConfigureRazorFormsInputOptions(options);
	}

	public static IServiceCollection ConfigureRazorFormsTextAreaOptions(this IServiceCollection self, ITextAreaOptions options) => self.AddSingleton(options);

	public static IServiceCollection ConfigureRazorFormsTextAreaOptions(this IServiceCollection self, Action<ITextAreaOptions> action)
	{
		var options = new TextAreaOptions();
		action(options);
		return self.ConfigureRazorFormsTextAreaOptions(options);
	}

	public static IServiceCollection ConfigureRazorFormsSelectOptions(this IServiceCollection self, ISelectOptions options) => self.AddSingleton(options);

	public static IServiceCollection ConfigureRazorFormsSelectOptions(this IServiceCollection self, Action<ISelectOptions> action)
	{
		var options = new SelectOptions();
		action(options);
		return self.ConfigureRazorFormsSelectOptions(options);
	}

	public static IServiceCollection ConfigureRazorFormsButtonOptions(this IServiceCollection self, IButtonOptions options) => self.AddSingleton(options);

	public static IServiceCollection ConfigureRazorFormsButtonOptions(this IServiceCollection self, Action<IButtonOptions> action)
	{
		var options = new ButtonOptions();
		action(options);
		return self.ConfigureRazorFormsButtonOptions(options);
	}
}