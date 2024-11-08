using Data.EF.Models;
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

    [HttpGet]
    public IActionResult Index()
    {
        _statsService.IncrementStatsCounter();
        return Ok("This is a protected resource.");
    }

}
