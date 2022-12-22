using Microsoft.AspNetCore.Authentication.Cookies;
using TicaretSitesi_yazlab.Models;
using TicaretSitesi_yazlab.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.Cookie.Name = "NetCoreMVc.Auth";
    option.LoginPath = "/Login/Index";
    option.AccessDeniedPath = "/Login/Index";

});
builder.Services.Configure<LaptopStoreDatabaseSettings>(
    builder.Configuration.GetSection("LaptopStoreDatabase"));
builder.Services.AddSingleton<LaptopService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

builder.Services.AddControllersWithViews();




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
