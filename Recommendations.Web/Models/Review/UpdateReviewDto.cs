using AutoMapper;
using Recommendations.Application.CommandsQueries.Review.Commands.Update;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Web.Models.Review;

public class UpdateReviewDto : IMapWith<UpdateReviewCommand>
{
    public Guid ReviewId { get; set; }
    public string Title { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int AuthorRate { get; set; }
    public IFormFile[]? Images { get; set; }
    public string[] Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateReviewDto, UpdateReviewCommand>();
    }
}