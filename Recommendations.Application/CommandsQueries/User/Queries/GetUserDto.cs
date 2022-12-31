using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.User.Queries;

public class GetUserDto : IMapWith<Domain.User>
{
    public Guid Id { get; set; }
    public int LikesCount { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.User, GetUserDto>()
            .ForMember(u => u.Id,
                o => o.MapFrom(u => u.Id))
            .ForMember(u => u.LikesCount,
                o => o.MapFrom(u => u.LikesCount))
            .ForMember(u => u.UserName,
                o => o.MapFrom(u => u.UserName))
            .ForMember(u => u.Email,
                o => o.MapFrom(u => u.Email));
    }
}