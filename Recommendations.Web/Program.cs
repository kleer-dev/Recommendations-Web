using System.Reflection;
using Recommendations.Application;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Application.Common.Mappings;
using Recommendations.Persistence;
using Recommendations.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var policyOptions = new CookiePolicyOptions { Secure = CookieSecurePolicy.Always };

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson();;

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IRecommendationsDbContext).Assembly));
});

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseCookiePolicy(policyOptions);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();