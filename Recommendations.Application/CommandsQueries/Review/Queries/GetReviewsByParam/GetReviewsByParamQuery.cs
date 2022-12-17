using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsByParam;

public class GetReviewsByParamQuery : IRequest<GetAllReviewsVm>
{
    public int Count { get; set; }
    public string? Filtrate { get; set; }
}