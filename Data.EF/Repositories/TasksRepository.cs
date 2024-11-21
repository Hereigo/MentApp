using Data.EF.Database;
using Data.EF.Models;
using Domain;
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

        public async Task CreateTaskAsync(TaskDetails task, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(task.FromDomain());
            await _toDoListDbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int taskId, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.FindAsync(taskId);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _toDoListDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskDetails>> GetAllTasksAsync(CancellationToken cancellationToken)
        {
            var entities = await _dbSet.ToListAsync();
            var tasks = entities.Select(x => x.ToDomain());
            return tasks;
        }

        public async Task<TaskDetails?> GetTaskByIdAsync(int taskId, CancellationToken cancellationToken)
        {
            var result = await _dbSet.FindAsync(taskId);
            return result?.ToDomain();
        }

        public async Task UpdateTaskAsync(TaskDetails task, CancellationToken cancellationToken)
        {
            _dbSet.Update(task.FromDomain());
            await _toDoListDbContext.SaveChangesAsync();
        }
    }
}
