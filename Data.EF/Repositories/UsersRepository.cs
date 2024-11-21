using Data.EF.Database;
using Data.EF.Models;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ToDoListDbContext _toDoListDbContext;
        private readonly DbSet<User> _dbSet;

        public UsersRepository(ToDoListDbContext toDoListDbContext)
        {
            _toDoListDbContext = toDoListDbContext;
            _dbSet = _toDoListDbContext.Set<User>();
        }

        public async Task<IEnumerable<UserDetails>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var dbUsers = await _dbSet.ToListAsync(cancellationToken);
            var domainUsers = dbUsers.Select(x => x.ToDomain());
            return domainUsers;
        }
    }
}
