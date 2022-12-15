using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Comment.Queries.Get;

public class GetCommentDto : IMapWith<Domain.Comment>
{
    public string AuthorName { get; set; }
    public DateTime CreationDate { get; set; }
    public string Text { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Comment, GetCommentDto>()
            .ForMember(u => u.AuthorName,
                o => o.MapFrom(u => u.User.UserName))
            .ForMember(u => u.CreationDate,
                o => o.MapFrom(u => u.CreationDate))
            .ForMember(u => u.Text,
                o => o.MapFrom(u => u.Text));
    }
}