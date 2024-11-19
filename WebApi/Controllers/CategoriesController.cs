using Contracts.Commands;
using Contracts.DTO;
using Contracts.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllCategorieAsync(CancellationToken cancellationToken)
    {
        var allCategories = await _mediator.Send(new GetAllCategoriesQuery() { }, cancellationToken);
        return allCategories == null ? NotFound() : Ok(allCategories);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategorieAsync(CategoryApiDto category, CancellationToken cancellationToken)
    {
        var newCategory = new CategoryDetails { Name = category.Name };

        await _mediator.Send(new CreateCategoryRequest(newCategory), cancellationToken);

        return Ok(category);
    }
}
