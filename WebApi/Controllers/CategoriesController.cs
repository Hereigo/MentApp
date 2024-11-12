using Contracts.Commands;
using Contracts.DTO;
using Contracts.Queries;
using Data.EF.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly SignInManager<User> _signInManager;
    private readonly StatsService _statsService;
    private readonly UserManager<User> _userManager;

    public CategoriesController(IMediator mediator, UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration configuration, StatsService statsService)
    {
        _configuration = configuration;
        _mediator = mediator;
        _signInManager = signInManager;
        _statsService = statsService;
        _userManager = userManager;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCategorieAsync(CancellationToken cancellationToken)
    {
        var allCategories = await _mediator.Send(new GetAllCategoriesQuery() { }, cancellationToken);
        return allCategories == null ? NotFound() : Ok(allCategories);
    }

    [HttpPost("new")]
    public async Task<IActionResult> CreateCategorieAsync(CategoryApiDto category, CancellationToken cancellationToken)
    {
        var newCategory = new CategoryDetails { Name = category.Name };

        await _mediator.Send(new CreateCategoryRequest() { Category = newCategory }, cancellationToken);

        return Ok(category);
    }
}
