using MediatR;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Tag.Commands.Create;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Unit>
{
    private readonly IRecommendationsDbContext _context;

    public CreateTagCommandHandler(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateTagCommand request,
        CancellationToken cancellationToken)
    {
        var uniqueTags = request.Tags
            .Except(_context.Tags.Select(t => t.Name));

        await AddTags(uniqueTags, cancellationToken);

        return Unit.Value;
    }

    private async Task AddTags(IEnumerable<string> tags,
        CancellationToken cancellationToken)
    {
        foreach (var tagName in tags)
        {
            var tag = new Domain.Tag
            {
                Name = tagName
            };

            await _context.Tags.AddAsync(tag, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}