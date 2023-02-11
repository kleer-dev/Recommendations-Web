using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Category.Queries.GetAll;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : BaseController
{
    public CategoryController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper) { }
    
    [HttpGet]
    public async Task<ActionResult<GetAllCategoriesDto>> GetAll()
    {
        var getAllCategoriesQuery = new GetAllCategoriesQuery();
        var categoriesVm = await Mediator.Send(getAllCategoriesQuery);
        
        return Ok(categoriesVm.Categories);
    }
}