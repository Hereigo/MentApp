using Data.EF.Models;

namespace Data.EF.Repositories;

public interface ITasksRepository
{
    Task CreateTaskAsync(ATask task);

    Task DeleteTaskAsync(int id);

    Task UpdateTaskAsync(ATask task);

    Task<ATask?> GetTaskByIdAsync(int taskId);

    Task<IEnumerable<ATask>> GetAllTasksAsync();
}
