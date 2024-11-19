using Domain;
using MediatR;

namespace Contracts.Queries
{
    public record GetAllTasksQuery : IRequest<IEnumerable<TaskDetails>>;
}
