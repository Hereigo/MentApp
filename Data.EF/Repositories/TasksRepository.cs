using Data.EF.Models;

namespace Data.EF.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        public Task<int> CreateTask(ATask task)
        {
            throw new NotImplementedException();
        }

        public Task<ATask?> GetTask(int taskId)
        {
            throw new NotImplementedException();
        }
    }
}
