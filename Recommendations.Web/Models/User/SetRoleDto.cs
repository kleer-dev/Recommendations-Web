using AutoMapper;
using Recommendations.Application.CommandsQueries.User.Commands.SetRole;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Web.Models.User;

public class SetRoleDto : IMapWith<SetUserRoleCommand>
{
    public Guid UserId { get; set; }
    public string Role { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetRoleDto, SetUserRoleCommand>();
    }
}