using Domain.Tasks;
using MediatR;

namespace Contracts.Queries
{
    public class GetAllTasksQuery : IRequest<IEnumerable<TaskDetails>>
    {
    }
}
