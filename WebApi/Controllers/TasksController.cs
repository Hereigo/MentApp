using Contracts.Commands;
using Contracts.DTO;
using Contracts.Queries;
using Data.EF.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;

    public TasksController(IMediator mediator, UserManager<User> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        // Just Testing usersRoles:
        var currentUser = await _userManager.GetUserAsync(this.User);
        var isSuperAdmin = await _userManager.IsInRoleAsync(currentUser, "SuperAdmin");

        var allTasks = await _mediator.Send(new GetAllTasksQuery() { }, cancellationToken);
        return allTasks == null ? NotFound() : Ok(allTasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskAsync(int id, CancellationToken cancellationToken)
    {
        var taskDetails = await _mediator.Send(new GetTaskQuery() { TaskId = id }, cancellationToken);
        return taskDetails == null ? NotFound() : Ok(taskDetails);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskSimpleAsync(TaskApiDto taskDto, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.GetUserAsync(this.User);
        if (currentUser != null)
        {
            var task = new TaskDetails
            {
                CategoryId = 1,
                CreatedDate = DateTime.UtcNow,
                Description = taskDto.Description,
                Title = taskDto.Title,
                UserId = currentUser.Id,
            };

            await _mediator.Send(new CreateTaskRequest() { Task = task }, cancellationToken);
        }
        return Ok(taskDto);
    }
}
