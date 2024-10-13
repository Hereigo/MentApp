using Data.EF.Models;
using Data.EF.Repositories;
using MediatR;

namespace Services.MediatR.Todos.Queries
{
    public class GetTaskHandler : IRequestHandler<GetTaskRequest, ATask?>
    {
        private readonly ITasksRepository _tasksRepository;

        public GetTaskHandler(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<ATask?> Handle(GetTaskRequest request, CancellationToken cancellationToken)
        {
            return await _tasksRepository.GetTaskByIdAsync(request.TaskId);
        }
    }
}
