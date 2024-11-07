using Data.EF.Database;
using Data.EF.Models;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.Repositories
{
    internal class CategoriesRepository : ICategoriesRepository
    {
        private readonly ToDoListDbContext _toDoListDbContext;
        private readonly DbSet<Category> _dbSet;

        public CategoriesRepository(ToDoListDbContext toDoListDbContext)
        {
            _toDoListDbContext = toDoListDbContext;
            _dbSet = _toDoListDbContext.Set<Category>();
        }

        public async Task CreateCategoryAsync(CategoryDetails category)
        {
            await _dbSet.AddAsync(category.FromDomain());
            await _toDoListDbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _toDoListDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CategoryDetails>> GetAllCategoriesAsync()
        {
            var entities = await _dbSet.ToListAsync();
            var tasks = entities.Select(x => x.ToDomain());
            return tasks;
        }

        public async Task<CategoryDetails> GetCategoryByIdAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);
            return result?.ToDomain();
        }

        public async Task UpdateCategoryAsync(CategoryDetails category)
        {
            _dbSet.Update(category.FromDomain());
            await _toDoListDbContext.SaveChangesAsync();
        }
    }
}
