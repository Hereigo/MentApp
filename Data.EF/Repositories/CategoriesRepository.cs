using Data.EF.Database;
using Data.EF.Models;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ToDoListDbContext _toDoListDbContext;
        private readonly DbSet<Category> _dbSet;

        public CategoriesRepository(ToDoListDbContext toDoListDbContext)
        {
            _toDoListDbContext = toDoListDbContext;
            _dbSet = _toDoListDbContext.Set<Category>();
        }

        public async Task CreateCategoryAsync(CategoryDetails category, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(category.FromDomain(), cancellationToken);
            await _toDoListDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.FindAsync(id, cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _toDoListDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<CategoryDetails>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            var entities = await _dbSet.ToListAsync(cancellationToken);
            var tasks = entities.Select(x => x.ToDomain());
            return tasks;
        }

        public async Task<CategoryDetails> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _dbSet.FindAsync(id, cancellationToken);
            return result?.ToDomain();
        }

        public async Task UpdateCategoryAsync(CategoryDetails category, CancellationToken cancellationToken)
        {
            _dbSet.Update(category.FromDomain());
            await _toDoListDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
