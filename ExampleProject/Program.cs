using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorForms;
using RazorForms.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
	.AddRazorRuntimeCompilation();

builder.Services.UseRazorForms(CustomSetup);

// builder.Services.UseRazorFormsWithMaterialize(MaterializeSetup);

// builder.Services.UseRazorFormsWithBulma(BulmaSetup);

// builder.Services.UseRazorFormsWithBootstrap5(Bootstrap5Setup);
// builder.Services.UseRazorFormsWithBootstrap5FloatingLabels(Bootstrap5Setup);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();

static void CustomSetup(RazorFormsOptions o)
{
	var validationOptions = new FormComponentWithValidationOptions
	{
		TemplatePath = RazorFormsExtensions.ValidityAwareContentPartial,
		AlwaysShowErrorContainer = true,
		ComponentWrapperClasses = "component",
		ErrorWrapperClasses = "error-wrapper",
		InputBlockWrapperClasses = "input-block",
		InputWrapperClasses = "input-wrapper",
		LabelWrapperClasses = "label-wrapper"
	};

	var standardOptions = new FormComponentOptions
	{
		TemplatePath = RazorFormsExtensions.ContentPartial,
		ComponentWrapperClasses = "component",
		InputBlockWrapperClasses = "input-block",
		InputWrapperClasses = "input-wrapper",
		LabelWrapperClasses = "label-wrapper"
	};

	o.InputOptions = validationOptions;
	o.SelectOptions = validationOptions;
	o.TextAreaOptions = validationOptions;
	o.CheckInputGroupOptions = validationOptions;
	o.RadioInputGroupOptions = validationOptions;
	o.CheckInputOptions = standardOptions;
	o.RadioInputOptions = standardOptions;
}

static void Bootstrap5Setup(RazorFormsOptions o)
{
	// Text
	o.InputOptions.ComponentWrapperClasses = "mb-3";
	o.InputOptions.ErrorWrapperClasses = $"{o.InputOptions.ErrorWrapperClasses} mt-1";
	o.InputOptions.ErrorClasses = "small";
	o.InputOptions.AlwaysShowErrorContainer = true;

	// TextArea
	o.TextAreaOptions.ComponentWrapperClasses = "mb-3";
	o.TextAreaOptions.ErrorWrapperClasses = $"{o.InputOptions.ErrorWrapperClasses} mt-1";
	o.TextAreaOptions.ErrorClasses = "small";
	o.TextAreaOptions.AlwaysShowErrorContainer = true;

	// Select
	o.SelectOptions.ComponentWrapperClasses = "mb-3";
	o.SelectOptions.ErrorWrapperClasses = $"{o.SelectOptions.ErrorWrapperClasses} mt-1";
	o.SelectOptions.ErrorClasses = "small";
	o.SelectOptions.AlwaysShowErrorContainer = true;

	// Radio/checkbox
	o.CheckInputGroupOptions.ComponentWrapperClasses = "mb-3";
	o.CheckInputGroupOptions.AlwaysShowErrorContainer = true;
	o.RadioInputGroupOptions.ComponentWrapperClasses = "mb-3";
	o.RadioInputGroupOptions.AlwaysShowErrorContainer = true;
}

static void BulmaSetup(RazorFormsOptions o)
{
	
}

static void MaterializeSetup(RazorFormsOptions o)
{
	
}