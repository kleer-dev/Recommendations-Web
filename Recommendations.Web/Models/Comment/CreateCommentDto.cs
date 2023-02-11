using AutoMapper;
using Recommendations.Application.CommandsQueries.Comment.Commands;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Web.Models.Comment;

public class CreateCommentDto : IMapWith<CreateCommentCommand>
{
    public Guid ReviewId { get; set; }
    public string Text { get; set; }
    public Guid? UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCommentDto, CreateCommentCommand>()
            .ForMember(u => u.ReviewId,
                o => o.MapFrom(u => u.ReviewId))
            .ForMember(u => u.Text,
                o => o.MapFrom(u => u.Text));
    }
}