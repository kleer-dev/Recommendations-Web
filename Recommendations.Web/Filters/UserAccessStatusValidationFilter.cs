using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Common.Constants;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Domain;

namespace Recommendations.Web.Filters;

public class UserAccessStatusValidationFilter : Attribute, IAsyncActionFilter
{
    private readonly SignInManager<User> _signInManager;
    private readonly IMediator _mediator;

    public UserAccessStatusValidationFilter(SignInManager<User> signInManager,
        IMediator mediator)
    {
        _signInManager = signInManager;
        _mediator = mediator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var userId = GetUserId(context);
        if (userId is null)
        {
            await next();
            return;
        }

        try
        {
            var user = await GetUser(userId!.Value);
            if (user.AccessStatus == UserAccessStatuses.Blocked)
            {
                await _signInManager.SignOutAsync();
                throw new AccessDeniedException($"The user with id: {user.Id} has been blocked");
            }
        }
        catch (NotFoundException e)
        {
            await next();
        }

        await next();
    }

    private Guid? GetUserId(ActionContext context)
    {
        var claim = context.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier);
        if (claim is null)
            return null;
        return Guid.Parse(claim);
    }

    private async Task<User> GetUser(Guid userId)
    {
        var getUserQuery = new GetUserQuery(userId);
        return await _mediator.Send(getUserQuery);
    }
}