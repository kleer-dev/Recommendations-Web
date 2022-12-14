using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Category.Queries.GetAll;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : BaseController
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetAllCategoriesDto>> GetAll()
    {
        var getAllCategoriesQuery = new GetAllCategoriesQuery();
        var categoriesVm = await _mediator.Send(getAllCategoriesQuery);
        
        return Ok(categoriesVm.Categories);
    }
}