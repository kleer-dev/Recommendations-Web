using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Recommendations.Application.Common.Constants;
using Recommendations.Domain;

namespace Recommendations.Persistence.Initializers;

public class AdminInitializer
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AdminInitializer(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task InitializeAsync()
    {
        if (await _userManager.FindByNameAsync(Roles.Admin) != null)
            return;
        
        var userName = _configuration["Admin:Login"];
        var email = _configuration["Admin:Email"];
        var password = _configuration["Admin:Password"];
        
        if (userName is null || email is null || password is null)
            throw new NullReferenceException("Administrator's data is not set");

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
            throw new NullReferenceException("The admin role does not exist");

        await _userManager.AddToRoleAsync(admin, Roles.Admin);
    }
}