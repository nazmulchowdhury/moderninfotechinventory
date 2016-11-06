using System.Collections.Generic;
using Data.Repositories.Product;
using Model.Product;

namespace Service.Product
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryRepository subCategoryRepository;

        public SubCategoryServices(ISubCategoryRepository subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public IEnumerable<SubCategoryEntity> GetAllSubCategories()
        {
            return subCategoryRepository.GetAll();
        }

        public SubCategoryEntity GetSubCategory(string subCategoryId)
        {
            return subCategoryRepository.GetById(subCategoryId);
        }

        public SubCategoryEntity CreateSubCategory(SubCategoryEntity subCategoryEntity)
        {
            return subCategoryRepository.Add(subCategoryEntity);
        }

        public bool UpdateSubCategory(string subCategoryId, SubCategoryEntity subCategoryEntity)
        {
            var storedItem = subCategoryRepository.GetById(subCategoryId);

            if (storedItem != null)
            {
                storedItem.SubCategoryName = subCategoryEntity.SubCategoryName;
                storedItem.CategoryId = subCategoryEntity.CategoryId;

                subCategoryRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteSubCategory(string subCategoryId)
        {
            return subCategoryRepository.Delete(subCategoryId);
        }
    }
}
