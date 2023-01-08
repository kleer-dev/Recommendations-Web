using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Product.Queries.GetAll;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : BaseController
{
    public ProductController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper) { }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<GetAllProductsDto>>> GetAll()
    {
        var getAllProductsQuery = new GetAllProductsQuery();
        var productsVm = await Mediator.Send(getAllProductsQuery);

        return Ok(productsVm.Products);
    }
}