using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Create;

public class CreateReviewCommand : IRequest<Guid>, IMapWith<Domain.Review>
{
    public Guid? UserId { get; set; }
    public string Title { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public int AuthorRate { get; set; }
    public IFormFile? Image { get; set; }
    public string[] Tags { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateReviewCommand, Domain.Review>()
            .ForMember(u => u.Title,
                o => o.MapFrom(u => u.Title))
            .ForMember(u => u.Category,
                o => o.MapFrom(u => u.Category))
            .ForMember(u => u.Description,
                o => o.MapFrom(u => u.Description))
            .ForMember(u => u.AuthorRate,
                o => o.MapFrom(u => u.AuthorRate))
            .ForMember(u => u.Tags,
                o => o.Ignore())
            .ForMember(u => u.Category,
            o => o.Ignore());
    }
}