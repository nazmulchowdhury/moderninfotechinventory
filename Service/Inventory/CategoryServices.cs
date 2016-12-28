using Model.Inventory;
using System.Collections.Generic;
using Data.Repositories.Inventory;

namespace Service.Inventory
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ICollection<CategoryEntity> GetAllCategories()
        {
            return categoryRepository.GetAll();
        }

        public CategoryEntity GetCategory(string categoryId)
        {
            return categoryRepository.GetById(categoryId);
        }

        public CategoryEntity CreateCategory(CategoryEntity categoryEntity)
        {
            return categoryRepository.Add(categoryEntity);
        }

        public bool UpdateCategory(string categoryId, CategoryEntity categoryEntity)
        {
            var storedItem = categoryRepository.GetById(categoryId);

            if (storedItem != null)
            {
                storedItem.CategoryName = categoryEntity.CategoryName;
                storedItem.TenantInfo.UserId = categoryEntity.TenantInfo.UserId;

                categoryRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCategory(string categoryId)
        {
            return categoryRepository.Delete(categoryId);
        }
    }
}
