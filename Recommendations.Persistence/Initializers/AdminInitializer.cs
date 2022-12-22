using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Recommendations.Domain;

namespace Recommendations.Persistence.Initializers;

public class AdminInitializer
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IConfiguration _configuration;

    public AdminInitializer(UserManager<User> userManager, IConfiguration configuration,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        if (await _userManager.FindByNameAsync("admin") != null)
            return;
        
        var userName = _configuration["Admin:Login"];
        var email = _configuration["Admin:Email"];
        var password = _configuration["Admin:Password"];
            
        if (userName is null)
            throw new NullReferenceException("Administrator's username not set");
        if (email is null)
            throw new NullReferenceException("Administrator's email not set");
        if (password is null)
            throw new NullReferenceException("Administrator's password not set");
            
        var user = new User
        {
            UserName = userName,
            Email = email
        };

        await _userManager.CreateAsync(user, password);
        await AddRole(userName);
    }

    private async Task AddRole(string name)
    {
        var admin = await _userManager.FindByNameAsync(name);
        if (admin is null) 
            throw new NullReferenceException("The admin does not exist");
        
        await _userManager.AddToRoleAsync(admin, "admin");
    }
}