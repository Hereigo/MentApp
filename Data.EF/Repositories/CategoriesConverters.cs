using Data.EF.Models;
using Domain;

namespace Data.EF.Repositories
{
    public static class CategoriesConverters
    {
        public static Category FromDomain(this CategoryDetails categoryDetails)
        {
            return new Category
            {
                Id = categoryDetails.Id,
                Name = categoryDetails.Name
            };
        }
        public static CategoryDetails ToDomain(this Category category)
        {
            return new CategoryDetails
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
