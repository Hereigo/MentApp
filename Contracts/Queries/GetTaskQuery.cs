using Domain;
using MediatR;

namespace Contracts.Queries
{
    public record GetTaskQuery(int TaskId) : IRequest<TaskDetails?>;
}
