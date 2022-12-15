using AutoMapper;
using Recommendations.Application.CommandsQueries.Rating.Commands.Set;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Web.Models.Rating;

public class SetRatingDto : IMapWith<SetRatingCommand>
{
    public int Value { get; set; }
    public Guid? UserId { get; set; }
    public Guid ReviewId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetRatingDto, SetRatingCommand>()
            .ForMember(u => u.Value,
                o => o.MapFrom(u => u.Value))
            .ForMember(u => u.UserId,
                o => o.Ignore())
            .ForMember(u => u.ReviewId,
                o => o.MapFrom(u => u.ReviewId));
    }
}