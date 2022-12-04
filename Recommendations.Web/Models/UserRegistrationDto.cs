using AutoMapper;
using Recommendations.Application.CommandsQueries.User.Commands.Registration;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Web.Models;

public class UserRegistrationDto : IMapWith<UserRegistrationCommand>
{
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Remember { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRegistrationDto, UserRegistrationCommand>()
            .ForMember(u => u.Login,
                o => o.MapFrom(u => u.Login))
            .ForMember(u => u.Email,
                o => o.MapFrom(u => u.Email))
            .ForMember(u => u.Password,
                o => o.MapFrom(u => u.Password))
            .ForMember(u => u.Remember,
                o => o.MapFrom(u => u.Remember));
    }
}