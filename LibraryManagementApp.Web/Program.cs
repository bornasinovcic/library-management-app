using LibraryManagementApp.DAL;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibraryDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("LibraryDbContext"),
			opt => opt.MigrationsAssembly("LibraryManagementApp.DAL")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var supportedCultures = new[]
{
    new CultureInfo("hr"),
    new CultureInfo("en-US")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("hr"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
        name: "placeAdd",
        pattern: "Place/Add",
        defaults: new { controller = "City", action = "Create" });

app.MapControllerRoute(
        name: "writerAdd",
        pattern: "Writer/Add",
        defaults: new { controller = "Author", action = "Create" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
