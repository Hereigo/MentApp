using Domain.Tasks;

namespace Data.EF.Repositories;

public interface ITasksRepository
{
    Task CreateTaskAsync(TaskDetails task);

    Task DeleteTaskAsync(int id);

    Task UpdateTaskAsync(TaskDetails task);

    Task<TaskDetails?> GetTaskByIdAsync(int taskId);

    Task<IEnumerable<TaskDetails>> GetAllTasksAsync();
}
