using Contracts.Commands;
using Contracts.DTO;
using Contracts.Queries;
using Domain;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

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
        CategoryValidator validator = new CategoryValidator();
        ValidationResult result = validator.Validate(category);
        if (!result.IsValid)
        {
            string errors = string.Empty;
            foreach (var failure in result.Errors)
            {
                errors += "\r\nProperty " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage;
            }
            return BadRequest(errors);
        }
        else
        {
            var newCategory = new CategoryDetails { Name = category.Name };
            await _mediator.Send(new CreateCategoryRequest(newCategory), cancellationToken);
            return Ok(category);
        }
    }
}
