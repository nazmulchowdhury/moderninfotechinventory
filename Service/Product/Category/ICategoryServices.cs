using System.Collections.Generic;
using Model.Product;

namespace Service.Product.Category
{
    public interface ICategoryServices
    {
        IEnumerable<CategoryEntity> GetAllCategories();
        CategoryEntity GetCategory(string categoryId);
        CategoryEntity CreateCategory(CategoryEntity categoryEntity);
        bool UpdateCategory(string categoryId, CategoryEntity categoryEntity);
        bool DeleteCategory(string categoryId);
    }
}
