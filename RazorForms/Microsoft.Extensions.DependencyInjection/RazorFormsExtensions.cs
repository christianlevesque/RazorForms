using System;
using RazorForms;
using RazorForms.Generators.Elements;
using RazorForms.Generators.Inputs;
using RazorForms.Options.Elements;
using RazorForms.Options.Inputs;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RazorFormsExtensions
{
	/// <summary>
	/// Adds RazorForms support using the supplied <see cref="RazorFormsOptions"/> instance
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The options to use when creating markup</param>
	/// <returns></returns>
	public static IServiceCollection UseRazorForms(this IServiceCollection self, RazorFormsOptions options)
	{
		return self.ConfigureRazorFormsCore()
		           .ConfigureRazorFormsInputOptions(options.InputOptions)
		           .ConfigureRazorFormsCheckInputOptions(options.CheckInputOptions)
		           .ConfigureRazorFormsRadioInputOptions(options.RadioInputOptions)
		           .ConfigureRazorFormsCheckInputGroupOptions(options.CheckInputGroupOptions)
		           .ConfigureRazorFormsRadioInputGroupOptions(options.RadioInputGroupOptions)
		           .ConfigureRazorFormsTextAreaOptions(options.TextAreaOptions)
		           .ConfigureRazorFormsSelectOptions(options.SelectOptions)
		           .ConfigureRazorFormsButtonOptions(options.ButtonOptions);
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

	/// <summary>
	/// Adds the core RazorForms services to the dependency container
	/// </summary>
	/// <remarks>
	/// This method is automatically called, so unless you're hacking the core, there's no need to call this method manually.
	/// </remarks>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsCore(this IServiceCollection self)
	{
		return self.AddTransient<ILabelGenerator, LabelGenerator>()
		           .AddTransient<IInputGenerator, InputGenerator>()
		           .AddTransient<ICheckInputGenerator, CheckInputGenerator>()
		           .AddTransient<IRadioInputGenerator, RadioInputGenerator>()
		           .AddTransient<ICheckRadioInputSectionGenerator, CheckRadioInputSectionGenerator>()
		           .AddTransient<ITextAreaGenerator, TextAreaGenerator>()
		           .AddTransient<IInputBlockWrapperGenerator, InputBlockWrapperGenerator>()
		           .AddTransient<ISelectGenerator, SelectGenerator>()
		           .AddTransient<IErrorGenerator, ErrorGenerator>()
		           .AddTransient<ISubmitButtonGenerator, SubmitButtonGenerator>()
		           .AddTransient<IResetButtonGenerator, ResetButtonGenerator>()
		           .AddTransient<IDefaultButtonGenerator, DefaultButtonGenerator>();
	}

	/// <summary>
	/// Adds an instance of <see cref="IInputOptions"/> to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The instance to add to the dependency container</param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsInputOptions(this IServiceCollection self, IInputOptions options) => self.AddSingleton(options);

	/// <summary>
	/// Configures an instance of <see cref="IInputOptions"/> and adds it to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default <see cref="IInputOptions"/></param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsInputOptions(this IServiceCollection self, Action<IInputOptions> action)
	{
		var options = new InputOptions();
		action(options);
		return self.ConfigureRazorFormsInputOptions(options);
	}

	/// <summary>
	/// Adds an instance of <see cref="ICheckInputOptions"/> to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The instance to add to the dependency container</param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsCheckInputOptions(this IServiceCollection self, ICheckInputOptions options) => self.AddSingleton(options);

	/// <summary>
	/// Configures an instance of <see cref="ICheckInputOptions"/> and adds it to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default <see cref="ICheckInputOptions"/></param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsCheckInputOptions(this IServiceCollection self, Action<ICheckInputOptions> action)
	{
		var options = new CheckInputOptions();
		action(options);
		return self.ConfigureRazorFormsCheckInputOptions(options);
	}

	/// <summary>
	/// Adds an instance of <see cref="IRadioInputOptions"/> to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The instance to add to the dependency container</param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsRadioInputOptions(this IServiceCollection self, IRadioInputOptions options) => self.AddSingleton(options);

	/// <summary>
	/// Configures an instance of <see cref="IRadioInputOptions"/> and adds it to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default <see cref="IRadioInputOptions"/></param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsRadioInputOptions(this IServiceCollection self, Action<IRadioInputOptions> action)
	{
		var options = new RadioInputOptions();
		action(options);
		return self.ConfigureRazorFormsRadioInputOptions(options);
	}

	/// <summary>
	/// Adds an instance of <see cref="ICheckInputGroupOptions"/> to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The instance to add to the dependency container</param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsCheckInputGroupOptions(this IServiceCollection self, ICheckInputGroupOptions options) => self.AddSingleton(options);

	/// <summary>
	/// Configures an instance of <see cref="ICheckInputGroupOptions"/> and adds it to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default <see cref="ICheckInputGroupOptions"/></param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsCheckInputGroupOptions(this IServiceCollection self, Action<ICheckInputGroupOptions> action)
	{
		var options = new CheckInputGroupOptions();
		action(options);
		return self.ConfigureRazorFormsCheckInputGroupOptions(options);
	}

	/// <summary>
	/// Adds an instance of <see cref="IRadioInputGroupOptions"/> to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The instance to add to the dependency container</param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsRadioInputGroupOptions(this IServiceCollection self, IRadioInputGroupOptions options) => self.AddSingleton(options);

	/// <summary>
	/// Configures an instance of <see cref="IRadioInputGroupOptions"/> and adds it to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default <see cref="IRadioInputGroupOptions"/></param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsRadioInputGroupOptions(this IServiceCollection self, Action<IRadioInputGroupOptions> action)
	{
		var options = new RadioInputGroupOptions();
		action(options);
		return self.ConfigureRazorFormsRadioInputGroupOptions(options);
	}

	/// <summary>
	/// Adds an instance of <see cref="ITextAreaOptions"/> to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The instance to add to the dependency container</param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsTextAreaOptions(this IServiceCollection self, ITextAreaOptions options) => self.AddSingleton(options);

	/// <summary>
	/// Configures an instance of <see cref="ITextAreaOptions"/> and adds it to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default <see cref="ITextAreaOptions"/></param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsTextAreaOptions(this IServiceCollection self, Action<ITextAreaOptions> action)
	{
		var options = new TextAreaOptions();
		action(options);
		return self.ConfigureRazorFormsTextAreaOptions(options);
	}

	/// <summary>
	/// Adds an instance of <see cref="ISelectOptions"/> to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The instance to add to the dependency container</param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsSelectOptions(this IServiceCollection self, ISelectOptions options) => self.AddSingleton(options);

	/// <summary>
	/// Configures an instance of <see cref="ISelectOptions"/> and adds it to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default <see cref="ISelectOptions"/></param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsSelectOptions(this IServiceCollection self, Action<ISelectOptions> action)
	{
		var options = new SelectOptions();
		action(options);
		return self.ConfigureRazorFormsSelectOptions(options);
	}

	/// <summary>
	/// Adds an instance of <see cref="IButtonOptions"/> to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="options">The instance to add to the dependency container</param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsButtonOptions(this IServiceCollection self, IButtonOptions options) => self.AddSingleton(options);

	/// <summary>
	/// Configures an instance of <see cref="IButtonOptions"/> and adds it to the dependency container
	/// </summary>
	/// <param name="self">The <see cref="IServiceCollection"/> instance</param>
	/// <param name="action">An <see cref="Action"/> that can be used to mutate the default <see cref="IButtonOptions"/></param>
	/// <returns></returns>
	public static IServiceCollection ConfigureRazorFormsButtonOptions(this IServiceCollection self, Action<IButtonOptions> action)
	{
		var options = new ButtonOptions();
		action(options);
		return self.ConfigureRazorFormsButtonOptions(options);
	}
}