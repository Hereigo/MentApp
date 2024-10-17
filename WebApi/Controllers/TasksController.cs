using Contracts.Categories;
using Data.EF.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.MediatR.Todos.Commands;
using Services.MediatR.Todos.Queries;

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
            var taskDetails = await _mediator.Send(new GetTaskRequest() { TaskId = taskId }, cancellationToken);

            return taskDetails as IActionResult; // ???
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync(ATask task, CancellationToken cancellationToken)
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
                var task = new ATask
                {
                    CreatedDate = DateTime.UtcNow,
                    Description = taskDto.Description,
                    Title = taskDto.Title,
                    User = currentUser
                };

                await _mediator.Send(new CreateTaskRequest() { Task = task }, cancellationToken);
            }

            return Ok(taskDto);
        }
    }
}
