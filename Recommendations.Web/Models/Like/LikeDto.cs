using AutoMapper;
using Recommendations.Application.CommandsQueries.Like.Commands.Set;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Web.Models.Like;

public class LikeDto : IMapWith<SetLikeCommand>
{
    public Guid? UserId { get; set; }
    public Guid ReviewId { get; set; }
    public bool IsLike { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<LikeDto, SetLikeCommand>()
            .ForMember(u => u.ReviewId,
                o => o.MapFrom(u => u.ReviewId))
            .ForMember(u => u.IsLike,
                o => o.MapFrom(u => u.IsLike));
    }
}