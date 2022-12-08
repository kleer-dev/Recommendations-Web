using MediatR;

namespace Recommendations.Application.CommandsQueries.Tag.Commands.Create;

public class CreateTagCommand : IRequest<Unit>
{
    public string[] Tags { get; set; }
}