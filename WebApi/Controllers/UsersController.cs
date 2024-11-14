using Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var allUsers = await _mediator.Send(new GetAllUsersQuery() { }, cancellationToken);
            return allUsers == null ? NotFound() : Ok(allUsers);
        }
    }
}
