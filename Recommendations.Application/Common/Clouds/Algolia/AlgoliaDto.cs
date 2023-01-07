using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.Common.Clouds.Algolia;

public class AlgoliaDto : IMapWith<Domain.Review>
{
    public string ObjectID { get; set; }
    public DateTime CreationDate { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string[] Tags { get; set; }
    public string CategoryName { get; set; }
    public string ProductName { get; set; }
    public double AverageRating { get; set; }
    public string[] CommentTexts { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, AlgoliaDto>()
            .ForMember(u => u.ObjectID,
                o => o.MapFrom(u => u.Id.ToString()))
            .ForMember(u => u.CategoryName,
                o => o.MapFrom(u => u.Category.Name))
            .ForMember(u => u.ProductName,
                o => o.MapFrom(u => u.Product.Name))
            .ForMember(u => u.AverageRating,
                o => o.MapFrom(u => u.Product.AverageRate))
            .ForMember(u => u.CommentTexts,
                o => o.MapFrom(u => u.Comments
                    .Select(c => c.Text)));
    }
}