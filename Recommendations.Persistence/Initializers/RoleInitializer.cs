using Microsoft.AspNetCore.Identity;
using Recommendations.Application.Common.Constants;

namespace Recommendations.Persistence.Initializers;

public class RoleInitializer
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleInitializer(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        var roles = new[] { Roles.Admin, Roles.User };
        
        foreach (var role in roles)
        {
            if (await _roleManager.RoleExistsAsync(role))
                continue;
            var identityRole = new IdentityRole<Guid>(role);
            await _roleManager.CreateAsync(identityRole);
        }
    }
}