using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetAll;

public class GetAllReviewsDto : IMapWith<Domain.Review>
{
    public Guid Id { get; set; }
    public string ReviewTitle { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public int AverageRate { get; set; }
    public List<string> Tags { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, GetAllReviewsDto>()
            .ForMember(u => u.Id,
                o => o.MapFrom(u => u.Id))
            .ForMember(u => u.ReviewTitle,
                o => o.MapFrom(u => u.Title))
            .ForMember(u => u.Category,
                o => o.MapFrom(u => u.Category.Name))
            .ForMember(u => u.Tags,
                o => o.MapFrom(u => 
                    u.Tags.Select(t => t.Name)));
    }
}