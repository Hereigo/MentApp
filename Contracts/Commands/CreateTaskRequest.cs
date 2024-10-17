using Domain.Tasks;
using MediatR;

namespace Contracts.Commands
{
    public class CreateTaskRequest : IRequest
    {
        public TaskDetails Task { get; set; }
    }
}
