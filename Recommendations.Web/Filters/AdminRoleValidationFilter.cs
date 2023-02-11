using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Recommendations.Application.Common.Constants;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Domain;

namespace Recommendations.Web.Filters;

public class AdminRoleValidationFilter : Attribute, IAsyncActionFilter
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AdminRoleValidationFilter(UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
        {

            var attribute = actionDescriptor.MethodInfo
                .GetCustomAttribute<AuthorizeAttribute>();
            if (attribute?.Roles is null)
            {
                await next();
                return;
            }
            if (attribute.Roles.Contains(Roles.Admin))
                await CheckRole(context);
        }
        
        await next();
    }

    private async Task CheckRole(ActionContext context)
    {
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return;
        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains(Roles.Admin))
        {
            await _signInManager.SignOutAsync();
            throw new AccessDeniedException("User is not an admin");
        }
    }
}