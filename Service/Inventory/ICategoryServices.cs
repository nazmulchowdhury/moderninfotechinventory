using System.Collections.Generic;
using Model.Inventory;

namespace Service.Inventory
{
    public interface ICategoryServices
    {
        ICollection<CategoryEntity> GetAllCategories();
        CategoryEntity GetCategory(string categoryId);
        CategoryEntity CreateCategory(CategoryEntity categoryEntity);
        bool UpdateCategory(string categoryId, CategoryEntity categoryEntity);
        bool DeleteCategory(string categoryId);
    }
}
