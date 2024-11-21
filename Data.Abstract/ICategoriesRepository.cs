using Domain;

namespace Data.EF.Repositories;

public interface ICategoriesRepository
{
    Task CreateCategoryAsync(CategoryDetails category, CancellationToken cancellationToken);

    Task DeleteCategoryAsync(int id, CancellationToken cancellationToken);

    Task UpdateCategoryAsync(CategoryDetails category, CancellationToken cancellationToken);

    Task<CategoryDetails> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<CategoryDetails>> GetAllCategoriesAsync(CancellationToken cancellationToken);
}
