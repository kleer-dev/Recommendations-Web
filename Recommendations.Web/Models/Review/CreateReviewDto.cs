using AutoMapper;
using Recommendations.Application.CommandsQueries.Review.Commands.Create;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Web.Models.Review;

public class CreateReviewDto : IMapWith<CreateReviewDto>
{
    public string Title { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int AuthorRate { get; set; }
    public IFormFile[]? Images { get; set; }
    public string[] Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateReviewDto, CreateReviewCommand>()
            .ForMember(u => u.Title,
                o => o.MapFrom(u => u.Title))
            .ForMember(u => u.ProductName,
                o => o.MapFrom(u => u.ProductName))
            .ForMember(u => u.CategoryName,
                o => o.MapFrom(u => u.CategoryName))
            .ForMember(u => u.Description,
                o => o.MapFrom(u => u.Description))
            .ForMember(u => u.AuthorRate,
                o => o.MapFrom(u => u.AuthorRate))
            .ForMember(u => u.Images,
                o => o.MapFrom(u => u.Images))
            .ForMember(u => u.Tags,
                o => o.MapFrom(u => u.Tags));
    }
}