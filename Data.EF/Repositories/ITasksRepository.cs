using Data.EF.Models;

namespace Data.EF.Repositories;

public interface ITasksRepository
{
    Task<int> CreateTask(ATask task);
    Task<ATask?> GetTask(int taskId);
}
