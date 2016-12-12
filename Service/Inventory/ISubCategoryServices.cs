using System.Collections.Generic;
using Model.Inventory;

namespace Service.Inventory
{
    public interface ISubCategoryServices
    {
        ICollection<SubCategoryEntity> GetAllSubCategories();
        ICollection<SubCategoryEntity> GetAllSubCategories(string categoryId);
        SubCategoryEntity GetSubCategory(string subCategoryId);
        SubCategoryEntity CreateSubCategory(SubCategoryEntity subCategoryEntity);
        bool UpdateSubCategory(string subCategoryId, SubCategoryEntity subCategoryEntity);
        bool DeleteSubCategory(string subCategoryId);
    }
}
