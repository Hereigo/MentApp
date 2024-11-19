using Domain;
using MediatR;

namespace Contracts.Commands
{
    public record CreateTaskRequest(TaskDetails Task) : IRequest;
}
