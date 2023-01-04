using MediatR;

namespace Recommendations.Application.CommandsQueries.Category.Queries.GetByName;

public class GetCategoryByNameQuery : IRequest<Domain.Category>
{
    public string Name { get; set; }

    public GetCategoryByNameQuery(string name)
    {
        Name = name;
    }
}