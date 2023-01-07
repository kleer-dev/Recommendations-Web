using System.Reflection;
using Recommendations.Application;
using Recommendations.Application.Common.Mappings;
using Recommendations.Application.Hubs;
using Recommendations.Application.Interfaces;
using Recommendations.Persistence;
using Recommendations.Web.Filters;
using Recommendations.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var policyOptions = new CookiePolicyOptions { Secure = CookieSecurePolicy.Always };

builder.Configuration.AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
builder.Configuration
    .AddJsonFile("/etc/secrets/secrets.json", true);

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson();

builder.Services.AddSignalR();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IRecommendationsDbContext).Assembly));
});

builder.Services.AddApplication(builder.Configuration);
await builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddScoped<UserAccessStatusValidationFilter>();
builder.Services.AddScoped<AdminRoleValidationFilter>();

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

app.MapHub<CommentHub>("/comments");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();