using Domain;

namespace Data.EF.Repositories;

public interface ICategoriesRepository
{
    Task CreateCategoryAsync(CategoryDetails category);

    Task DeleteCategoryAsync(int id);

    Task UpdateCategoryAsync(CategoryDetails category);

    Task<CategoryDetails> GetCategoryByIdAsync(int id);

    Task<IEnumerable<CategoryDetails>> GetAllCategoriesAsync();
}
