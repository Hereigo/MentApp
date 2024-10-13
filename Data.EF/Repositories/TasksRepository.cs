using Data.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ToDoListDbContext _toDoListDbContext;
        private readonly DbSet<ATask> _dbSet;

        public TasksRepository(ToDoListDbContext toDoListDbContext)
        {
            _toDoListDbContext = toDoListDbContext;
            _dbSet = _toDoListDbContext.Set<ATask>();
        }

        public async Task CreateTaskAsync(ATask task)
        {
            await _dbSet.AddAsync(task);
            await _toDoListDbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var entity = await GetTaskByIdAsync(taskId);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _toDoListDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ATask>> GetAllTasksAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<ATask?> GetTaskByIdAsync(int taskId)
        {
            return await _dbSet.FindAsync(taskId);
        }

        public async Task UpdateTaskAsync(ATask task)
        {
            _dbSet.Update(task);
            await _toDoListDbContext.SaveChangesAsync();
        }
    }
}
