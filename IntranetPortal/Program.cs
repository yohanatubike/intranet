
using IntranetPortal.CustomHandler;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IntranetDBContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("IntranetDBConnection")));
builder.Services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication",
    options =>
{
    options.Cookie.Name = "UserProfile";
    options.LoginPath = "/Home/Login";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("General", policyBuilder =>
    {
        policyBuilder.UserRequireCustomClaim(ClaimTypes.Email);
        policyBuilder.UserRequireCustomClaim(ClaimTypes.DateOfBirth);
    });
    config.AddPolicy("Supervisor", policyBuilder =>
    {
        policyBuilder.RequireClaim("IsSupervisor","True");
       
    });
    config.AddPolicy("Administrators", policyBuilder =>
    {
        policyBuilder.RequireClaim("IsAdmin", "ICT");

    });
    config.AddPolicy("ContentManagers", policyBuilder =>
    {
        policyBuilder.RequireClaim("IsAContentManager", "13");

    });
});

builder.Services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);//You can set Time   
}); ;
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
        app.UseStaticFiles();
       

        app.UseRouting();
        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
