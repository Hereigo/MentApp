using Contracts.Commands;
using Contracts.DTO;
using Contracts.Queries;
using Data.EF.Models;
using Domain.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public TasksController(IMediator mediator, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("This is a protected resource.");
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
        public async Task<IActionResult> CreateTaskSimpleAsync(ATaskSimpleDto taskDto, CancellationToken cancellationToken)
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
}
