using Contracts.Queries;
using Data.EF.Repositories;
using Domain;
using MediatR;

namespace Services.MediatR.Queries
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskDetails>>
    {
        private readonly ITasksRepository _tasksRepository;

        public GetAllTasksHandler(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<IEnumerable<TaskDetails>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return await _tasksRepository.GetAllTasksAsync(cancellationToken);
        }
    }
}
