using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

[Authorize(Roles = "SuperAdmin")]
[ApiController]
[Route("[controller]")]
public class StatsController : ControllerBase
{
    private readonly StatsService _statsService;

    public StatsController(StatsService statsService)
    {
        _statsService = statsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(_statsService.GetStatsCounter());
    }
}
