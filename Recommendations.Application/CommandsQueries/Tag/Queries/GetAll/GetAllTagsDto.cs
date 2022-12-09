using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Tag.Queries.GetAll;

public class GetAllTagsDto : IMapWith<Domain.Tag>
{
    public string Name { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Tag, GetAllTagsDto>()
            .ForMember(u => u.Name,
                o => o.MapFrom(u => u.Name));
    }
}