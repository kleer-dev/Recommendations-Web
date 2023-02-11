using MediatR;

namespace Recommendations.Application.CommandsQueries.Tag.Queries.GetTagListByNames;

public class GetTagListByNamesQuery : IRequest<IEnumerable<Domain.Tag>>
{
    public string[] Tags { get; set; }

    public GetTagListByNamesQuery(string[] tags)
    {
        Tags = tags;
    }
}