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
        if (await _roleManager.RoleExistsAsync(Roles.Admin))
            return;
        var role = new IdentityRole<Guid>(Roles.Admin);
        await _roleManager.CreateAsync(role);
    }
}