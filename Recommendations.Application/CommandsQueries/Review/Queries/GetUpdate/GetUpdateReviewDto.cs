using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetUpdate;

public class GetUpdateReviewDto : IMapWith<Domain.Review>
{
    public string Title { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int AuthorRate { get; set; }
    public string? ImageUrl { get; set; }
    public string[] Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, GetUpdateReviewDto>()
            .ForMember(u => u.Title,
                o => o.MapFrom(u => u.Title))
            .ForMember(u => u.ProductName,
                o => o.MapFrom(u => u.Product.Name))
            .ForMember(u => u.CategoryName,
                o => o.MapFrom(u => u.Category.Name))
            .ForMember(u => u.Description,
                o => o.MapFrom(u => u.Description))
            .ForMember(u => u.AuthorRate,
                o => o.MapFrom(u => u.AuthorRate))
            .ForMember(u => u.ImageUrl,
                o => o.MapFrom(u => u.ImageUrl))
            .ForMember(u => u.Tags,
                o => o.MapFrom(u => u.Tags
                    .Select(t => t.Name)));
    }
}