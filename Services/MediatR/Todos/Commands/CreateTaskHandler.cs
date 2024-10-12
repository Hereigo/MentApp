using Data.EF.Repositories;
using MediatR;

namespace Services.MediatR.Todos.Commands
{
    internal class CreateTaskHandler: IRequestHandler<CreateTaskRequest, int>
    {
        //Inject Validators 
        private readonly ITasksRepository _tasksRepository;

        public CreateTaskHandler(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<int> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            // First validate the request
            return await _tasksRepository.CreateTask(request.Task);
        }
    }
}
