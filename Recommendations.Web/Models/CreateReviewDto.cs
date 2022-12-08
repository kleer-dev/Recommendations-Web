using AutoMapper;
using Recommendations.Application.CommandsQueries.Review.Commands.Create;
using Recommendations.Application.CommandsQueries.User.Queries.Login;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Web.Models;

public class CreateReviewDto : IMapWith<CreateReviewDto>
{
    public string Title { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public int Rate { get; set; }
    public IFormFile? Image { get; set; }
    public string Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateReviewDto, CreateReviewCommand>()
            .ForMember(u => u.Title,
                o => o.MapFrom(u => u.Title))
            .ForMember(u => u.ProductName,
                o => o.MapFrom(u => u.ProductName))
            .ForMember(u => u.Category,
                o => o.MapFrom(u => u.Category))
            .ForMember(u => u.Description,
                o => o.MapFrom(u => u.Description))
            .ForMember(u => u.Rate,
                o => o.MapFrom(u => u.Rate))
            .ForMember(u => u.Image,
                o => o.MapFrom(u => u.Image))
            .ForMember(u => u.Tags,
                o => o.MapFrom(u => u.Tags.Split(new[] { ',' })));
    }
}