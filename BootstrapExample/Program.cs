using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.UseRazorFormsWithBootstrap(o =>
{
	o.InputOptions.ComponentWrapperClasses = "mb-3";
	o.InputOptions.InputBlockWrapperClasses = "form-floating";
	o.InputOptions.ErrorWrapperClasses = $"{o.InputOptions.ErrorWrapperClasses} mt-1";
	o.InputOptions.ErrorClasses = "small";
	o.InputOptions.InputFirst = true;
	o.InputOptions.RemoveWrappers = true;
	o.SelectOptions.ComponentWrapperClasses = "mb-3";
	o.SelectOptions.InputBlockWrapperClasses = "form-floating";
	o.SelectOptions.ErrorWrapperClasses = $"{o.SelectOptions.ErrorWrapperClasses} mt-1";
	o.SelectOptions.ErrorClasses = "small";
	o.SelectOptions.InputFirst = true;
	o.SelectOptions.RemoveWrappers = true;
});

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();