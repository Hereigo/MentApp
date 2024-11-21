using Contracts.Queries;
using Data.EF.Repositories;
using Domain;
using MediatR;

namespace Services.MediatR.Queries
{
    public class GetTaskHandler : IRequestHandler<GetTaskQuery, TaskDetails?>
    {
        private readonly ITasksRepository _tasksRepository;

        public GetTaskHandler(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<TaskDetails?> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            return await _tasksRepository.GetTaskByIdAsync(request.TaskId, cancellationToken);
        }
    }
}
