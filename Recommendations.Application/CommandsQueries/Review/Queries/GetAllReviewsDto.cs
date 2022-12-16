using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Review.Queries;

public class GetAllReviewsDto : IMapWith<Domain.Review>
{
    public Guid Id { get; set; }
    public string ReviewTitle { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public DateTime CreationDate { get; set; }
    public double AverageRate { get; set; }
    public List<string> Tags { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, GetAllReviewsDto>()
            .ForMember(u => u.Id,
                o => o.MapFrom(u => u.Id))
            .ForMember(u => u.ReviewTitle,
                o => o.MapFrom(u => u.Title))
            .ForMember(u => u.AverageRate,
                o => o.MapFrom(u => u.Product.UserRatings
                    .Select(r => r.Value).DefaultIfEmpty().Average()))
            .ForMember(u => u.Category,
                o => o.MapFrom(u => u.Category.Name))
            .ForMember(u => u.CreationDate,
                o => o.MapFrom(u => u.CreationDate))
            .ForMember(u => u.Tags,
                o => o.MapFrom(u => 
                    u.Tags.Select(t => t.Name)));
    }
}