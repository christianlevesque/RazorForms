using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorForms.Materialize.Options;
using RazorForms.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.UseRazorForms<CustomOptions>(CustomSetup);

// builder.Services.UseRazorFormsWithMaterialize<MaterializeOptions>(MaterializeSetup);

// builder.Services.UseRazorFormsWithBulma<RazorFormsOptions>(BulmaSetup);

// builder.Services.UseRazorFormsWithBootstrap5<RazorFormsOptions>(Bootstrap5Setup);
// builder.Services.UseRazorFormsWithBootstrap5FloatingLabels<RazorFormsOptions>(Bootstrap5Setup);

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

static void CustomSetup(CustomOptions o)
{
	var validationOptions = new ValidityAwareFormComponentOptions
	{
		AlwaysRenderErrorContainer = true,
		ComponentWrapperClasses = "component",
		ErrorWrapperClasses = "error-wrapper",
		InputBlockWrapperClasses = "input-block",
		InputWrapperClasses = "input-wrapper",
		LabelWrapperClasses = "label-wrapper"
	};

	var standardOptions = new FormComponentOptions
	{
		ComponentWrapperClasses = "component",
		InputWrapperClasses = "input-wrapper",
		LabelWrapperClasses = "label-wrapper"
	};

	o.TextInputOptions = validationOptions;
	o.SelectInputOptions = validationOptions;
	o.TextAreaInputOptions = validationOptions;
	o.CheckInputGroupOptions = validationOptions;
	o.RadioInputGroupOptions = validationOptions;
	o.CheckInputOptions = standardOptions;
	o.RadioInputOptions = standardOptions;
}

static void Bootstrap5Setup(RazorFormsOptions o)
{
	// Text
	o.TextInputOptions.ComponentWrapperClasses = "mb-3";
	o.TextInputOptions.ErrorWrapperClasses = "mt-1";
	o.TextInputOptions.ErrorClasses = "small";
	o.TextInputOptions.AlwaysRenderErrorContainer = true;

	// TextArea
	o.TextAreaInputOptions.ComponentWrapperClasses = "mb-3";
	o.TextAreaInputOptions.ErrorWrapperClasses = "mt-1";
	o.TextAreaInputOptions.ErrorClasses = "small";
	o.TextAreaInputOptions.AlwaysRenderErrorContainer = true;

	// Select
	o.SelectInputOptions.ComponentWrapperClasses = "mb-3";
	o.SelectInputOptions.ErrorWrapperClasses = "mt-1";
	o.SelectInputOptions.ErrorClasses = "small";
	o.SelectInputOptions.AlwaysRenderErrorContainer = true;

	// Radio/checkbox
	o.CheckInputGroupOptions.ComponentWrapperClasses = "mb-3";
	o.CheckInputGroupOptions.AlwaysRenderErrorContainer = true;
	o.RadioInputGroupOptions.ComponentWrapperClasses = "mb-3";
	o.RadioInputGroupOptions.AlwaysRenderErrorContainer = true;
}

static void BulmaSetup(RazorFormsOptions o)
{
	
}

static void MaterializeSetup(MaterializeOptions o)
{
	
}

internal class CustomOptions : RazorFormsOptions
{}