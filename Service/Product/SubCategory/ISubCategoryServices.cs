using System.Collections.Generic;
using Model.Product;

namespace Service.Product.SubCategory
{
    public interface ISubCategoryServices
    {
        IEnumerable<SubCategoryEntity> GetAllSubCategories();
        SubCategoryEntity GetSubCategory(string subCategoryId);
        SubCategoryEntity CreateSubCategory(SubCategoryEntity subCategoryEntity);
        bool UpdateSubCategory(string subCategoryId, SubCategoryEntity subCategoryEntity);
        bool DeleteSubCategory(string subCategoryId);
    }
}
