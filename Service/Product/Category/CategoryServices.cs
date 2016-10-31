using System.Collections.Generic;
using Data.Repositories.Product.Category;
using Model.Product;

namespace Service.Product.Category
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryEntity> GetAllCategories()
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
