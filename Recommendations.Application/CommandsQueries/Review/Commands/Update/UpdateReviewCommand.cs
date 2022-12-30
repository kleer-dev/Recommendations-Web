using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Update;

public class UpdateReviewCommand : IRequest, IMapWith<Domain.Review>
{
    public Guid ReviewId { get; set; }
    public string Title { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int AuthorRate { get; set; }
    public IFormFile[] Images { get; set; }
    public string[] Tags { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateReviewCommand, Domain.Review>()
            .ForMember(u => u.Title,
                o => o.MapFrom(u => u.Title))
            .ForMember(u => u.Description,
                o => o.MapFrom(u => u.Description))
            .ForMember(u => u.AuthorRate,
                o => o.MapFrom(u => u.AuthorRate))
            .ForMember(u => u.Images,
                o => o.Ignore())
            .ForMember(u => u.Tags,
                o => o.Ignore());
    }
}