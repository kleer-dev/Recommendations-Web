using AutoMapper;
using Recommendations.Application.CommandsQueries.User.Queries.Login;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Web.Models;

public class UserLoginDto : IMapWith<UserLoginQuery>
{
    public string Email { get; set; }
    public string Password { set; get; }
    public bool Remember { set; get; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserLoginDto, UserLoginQuery>()
            .ForMember(u => u.Email,
                o => o.MapFrom(u => u.Email))
            .ForMember(u => u.Password,
                o => o.MapFrom(u => u.Password))
            .ForMember(u => u.Remember,
                o => o.MapFrom(u => u.Remember));
    }
}