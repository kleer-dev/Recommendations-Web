using AutoMapper;
using Recommendations.Application.Common.Mappings;

namespace Recommendations.Application.CommandsQueries.Product.Queries.GetAll;

public class GetAllProductsDto : IMapWith<Domain.Product>
{
    public string Name { get; set; }
    public double AverageRating { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Product, GetAllProductsDto>()
            .ForMember(u => u.Name,
                o => o.MapFrom(u => u.Name))
            .ForMember(u => u.AverageRating,
                o => o.MapFrom(u => u.AverageRate));
    }
}