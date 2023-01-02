namespace Recommendations.Web.Models.User;

public class RoleDto
{
    public string? RoleName { get; set; }

    public RoleDto(string? roleName)
    {
        RoleName = roleName;
    }
}