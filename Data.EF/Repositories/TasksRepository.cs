using Data.EF.Database;
using Data.EF.Models;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ToDoListDbContext _toDoListDbContext;
        private readonly DbSet<ATask> _tasksDbSet;
        private readonly DbSet<User> _usersDbSet;

        public TasksRepository(ToDoListDbContext toDoListDbContext)
        {
            _toDoListDbContext = toDoListDbContext;
            _tasksDbSet = _toDoListDbContext.Set<ATask>();
            _usersDbSet = _toDoListDbContext.Set<User>();
        }

        public async Task CreateTaskAsync(TaskDetails task, CancellationToken cancellationToken)
        {
            using var TRANSACTION = _toDoListDbContext.Database.BeginTransaction();

            await _tasksDbSet.AddAsync(task.FromDomain());
            await _toDoListDbContext.SaveChangesAsync(cancellationToken);

            var currentUser = _usersDbSet.FirstOrDefault(u => u.Id == task.UserId);
            currentUser.TasksCount++;
            _usersDbSet.Update(currentUser);
            await _toDoListDbContext.SaveChangesAsync(cancellationToken);

            TRANSACTION.Commit();
        }

        public async Task DeleteTaskAsync(int taskId, string userId, CancellationToken cancellationToken)
        {
            var entity = await _tasksDbSet.FindAsync(taskId, cancellationToken);
            if (entity != null)
            {
                using var TRANSACTION = _toDoListDbContext.Database.BeginTransaction();

                _tasksDbSet.Remove(entity);
                await _toDoListDbContext.SaveChangesAsync(cancellationToken);

                var currentUser = _usersDbSet.FirstOrDefault(u => u.Id == userId);
                currentUser.TasksCount++;
                _usersDbSet.Update(currentUser);
                await _toDoListDbContext.SaveChangesAsync(cancellationToken);

                TRANSACTION.Commit();
            }
        }

        public async Task<IEnumerable<TaskDetails>> GetAllTasksAsync(CancellationToken cancellationToken)
        {
            var entities = await _tasksDbSet.ToListAsync(cancellationToken);
            var tasks = entities.Select(x => x.ToDomain());
            return tasks;
        }

        public async Task<TaskDetails?> GetTaskByIdAsync(int taskId, CancellationToken cancellationToken)
        {
            var result = await _tasksDbSet.FindAsync(taskId, cancellationToken);
            return result?.ToDomain();
        }

        public async Task UpdateTaskAsync(TaskDetails task, CancellationToken cancellationToken)
        {
            _tasksDbSet.Update(task.FromDomain());
            await _toDoListDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
