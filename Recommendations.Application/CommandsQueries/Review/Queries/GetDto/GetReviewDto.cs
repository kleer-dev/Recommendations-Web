using AutoMapper;
using Recommendations.Application.CommandsQueries.Review.Queries.GetLinkedReviewsByReviewId;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetDto;

public class GetReviewDto : IMapWith<Domain.Review>
{
    public string AuthorName { get; set; }
    public double AverageRate { get; set; }
    public int AuthorLikesCount { get; set; }
    public bool IsLike { get; set; }
    public double UserRating { get; set; }
    public int LikesCount { get; set; }
    public string ReviewTitle { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public int AuthorRate { get; set; }
    public string[] Tags { get; set; }
    public List<string>? ImagesUrls { get; set; }
    public DateTime CreationDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, GetReviewDto>()
            .ForMember(u => u.AuthorName,
                o => o.MapFrom(u => u.User.UserName))
            .ForMember(u => u.AuthorLikesCount,
                o => o.MapFrom(u => u.User.LikesCount))
            .ForMember(u => u.AverageRate,
                o => o.MapFrom(u => u.Product.AverageRate))
            .ForMember(u => u.ReviewTitle,
                o => o.MapFrom(u => u.Title))
            .ForMember(u => u.ProductName,
                o => o.MapFrom(u => u.Product.Name))
            .ForMember(u => u.Category,
                o => o.MapFrom(u => u.Category.Name))
            .ForMember(u => u.AuthorRate,
                o => o.MapFrom(u => u.AuthorRate))
            .ForMember(u => u.CreationDate,
                o => o.MapFrom(u => u.CreationDate))
            .ForMember(u => u.Description,
                o => o.MapFrom(u => u.Description))
            .ForMember(u => u.LikesCount,
                o => o.MapFrom(u => u.Likes
                    .Count(l => l.Status)))
            .ForMember(u => u.Tags,
                o => o.MapFrom(u => 
                    u.Tags.Select(t => t.Name)));
    }
}