using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsByParam;

public class GetReviewsByParamQuery : IRequest<GetAllReviewsVm>
{
    public int? Count { get; set; }
    public string? Filtrate { get; set; }
    public string? Tag { get; set; }

    public GetReviewsByParamQuery(int? count, string? filtrate, string? tag)
    {
        Count = count;
        Filtrate = filtrate;
        Tag = tag;
    }
}