﻿using Data.EF.Repositories;
using MediatR;

namespace Services.MediatR.Todos.Commands
{
    internal class CreateTaskHandler: IRequestHandler<CreateTaskRequest>
    {
        //Inject Validators 
        private readonly ITasksRepository _tasksRepository;

        public CreateTaskHandler(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            // First validate the request
            await _tasksRepository.CreateTaskAsync(request.Task);
        }
    }
}