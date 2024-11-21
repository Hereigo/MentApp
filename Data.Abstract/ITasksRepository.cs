using Domain;

namespace Data.EF.Repositories;

public interface ITasksRepository
{
    Task CreateTaskAsync(TaskDetails task, CancellationToken cancellationToken);

    Task DeleteTaskAsync(int id, string userId, CancellationToken cancellationToken);

    Task UpdateTaskAsync(TaskDetails task, CancellationToken cancellationToken);

    Task<TaskDetails?> GetTaskByIdAsync(int taskId, CancellationToken cancellationToken);

    Task<IEnumerable<TaskDetails>> GetAllTasksAsync(CancellationToken cancellationToken);
}
