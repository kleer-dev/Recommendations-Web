using AutoMapper;
using MediatR;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Comment.Commands;

public class CreateCommentCommand : IRequest<Guid>, IMapWith<Domain.Comment>
{
    public Guid? UserId { get; set; }
    public Guid ReviewId { get; set; }
    public string Text { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCommentCommand, Domain.Comment>()
            .ForMember(u => u.Text,
                o => o.MapFrom(u => u.Text))
            .ForMember(u => u.CreationDate,
                o =>  o.MapFrom(u => DateTime.UtcNow));
    }
}