using Microsoft.AspNetCore.Identity;

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
        if (await _roleManager.RoleExistsAsync("admin"))
            return;
        
        var role = new IdentityRole<Guid>("admin");
        await _roleManager.CreateAsync(role);
    }
}