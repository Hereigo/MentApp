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
public class TasksController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly SignInManager<User> _signInManager;
    private readonly StatsService _statsService;
    private readonly UserManager<User> _userManager;

    public TasksController(IMediator mediator, UserManager<User> userManager,
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

    [HttpGet("GetCounter")]
    public IActionResult GetCounter()
    {
        return Ok(_statsService.GetStatsCounter());
    }

    [HttpGet("customerId")]
    public async Task<IActionResult> GetTaskAsync(int taskId, CancellationToken cancellationToken)
    {
        var taskDetails = await _mediator.Send(new GetTaskQuery() { TaskId = taskId }, cancellationToken);

        return taskDetails == null ? NotFound() : Ok(taskDetails);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var taskDetails = await _mediator.Send(new GetAllTasksQuery() { }, cancellationToken);

        return taskDetails == null ? NotFound() : Ok(taskDetails);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync(TaskDetails task, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateTaskRequest() { Task = task }, cancellationToken);

        return Ok(task);
    }

    [HttpPost("Test")]
    public async Task<IActionResult> CreateTaskSimpleAsync(ATaskApiDto taskDto, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.GetUserAsync(this.User);

        if (currentUser != null)
        {
            var task = new TaskDetails
            {
                //CreatedDate = DateTime.UtcNow,
                Description = taskDto.Description,
                Title = taskDto.Title,
                UserId = currentUser.Id,
                // CategoryId = 

                // TODO :
                // Continue to implement ..............
            };

            await _mediator.Send(new CreateTaskRequest() { Task = task }, cancellationToken);
        }

        return Ok(taskDto);
    }
}
