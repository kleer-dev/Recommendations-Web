using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Category.Queries.GetAll;

public class GetAllCategoriesDto : IMapWith<Domain.Category>
{
    public string Name { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Category, GetAllCategoriesDto>()
            .ForMember(u => u.Name,
                o => o.MapFrom(u => u.Name));
    }
}