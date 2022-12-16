using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorForms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// builder.Services.UseRazorFormsWithMaterialize(MaterializeSetup);

// builder.Services.UseRazorFormsWithBulma(BulmaSetup);

// builder.Services.UseRazorFormsWithBootstrap5(Bootstrap5Setup);
builder.Services.UseRazorFormsWithBootstrap5FloatingLabels(Bootstrap5Setup);

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

static void Bootstrap5Setup(RazorFormsOptions o)
{
	// Text
	o.InputOptions.ComponentWrapperClasses = "mb-3";
	o.InputOptions.ErrorWrapperClasses = $"{o.InputOptions.ErrorWrapperClasses} mt-1";
	o.InputOptions.ErrorClasses = "small";

	// TextArea
	o.TextAreaOptions.ComponentWrapperClasses = "mb-3";
	o.TextAreaOptions.ErrorWrapperClasses = $"{o.InputOptions.ErrorWrapperClasses} mt-1";
	o.TextAreaOptions.ErrorClasses = "small";

	// Select
	o.SelectOptions.ComponentWrapperClasses = "mb-3";
	o.SelectOptions.ErrorWrapperClasses = $"{o.SelectOptions.ErrorWrapperClasses} mt-1";
	o.SelectOptions.ErrorClasses = "small";

	// Radio/checkbox
	o.CheckInputGroupOptions.ComponentWrapperClasses = "mb-3";
	o.RadioInputGroupOptions.ComponentWrapperClasses = "mb-3";
}

static void BulmaSetup(RazorFormsOptions o)
{
	
}

static void MaterializeSetup(RazorFormsOptions o)
{
	
}