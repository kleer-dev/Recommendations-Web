using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetLinkedReviewsByReviewId;

public class GetLinkedReviewsDto : IMapWith<Domain.Review>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, GetLinkedReviewsDto>();
    }
}