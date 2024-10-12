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
        public async Task<ATask?> GetCustomerAsync(int taskId)
        {
            var taskDetails = await _mediator.Send(new GetTaskRequest() { TaskId = taskId });

            return taskDetails;
        }

        [HttpPost]
        public async Task<int> CreateCustomerAsync(ATask task)
        {
            var taskId = await _mediator.Send(new CreateTaskRequest() { Task = task });
            return taskId;
        }
    }
}
