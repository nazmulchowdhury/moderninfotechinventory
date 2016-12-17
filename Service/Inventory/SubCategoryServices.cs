using System.Collections.Generic;
using Data.Repositories.Inventory;
using Model.Inventory;

namespace Service.Inventory
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryRepository subCategoryRepository;

        public SubCategoryServices(ISubCategoryRepository subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public ICollection<SubCategoryEntity> GetAllSubCategories()
        {
            return subCategoryRepository.GetAll();
        }

        public ICollection<SubCategoryEntity> GetAllSubCategories(string categoryId)
        {
            return subCategoryRepository.GetMany((SubCategoryEntity se) => se.CategoryId == categoryId);
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
                storedItem.UnitId = subCategoryEntity.UnitId;

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
