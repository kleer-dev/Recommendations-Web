using AutoMapper;
using MediatR;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.User.Commands.Registration;

public class UserRegistrationCommand : IRequest, IMapWith<Domain.User>
{
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Remember { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRegistrationCommand, Domain.User>()
            .ForMember(u => u.UserName,
                o => o.MapFrom(u => u.Login))
            .ForMember(u => u.Email,
                o => o.MapFrom(u => u.Email));
    }
}