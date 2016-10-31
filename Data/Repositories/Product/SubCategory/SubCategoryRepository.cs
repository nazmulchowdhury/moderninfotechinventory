using Data.Infrastructure;
using Data.Helper;
using Model.Product;
using System.Linq;

namespace Data.Repositories.Product.SubCategory
{
    public class SubCategoryRepository : RepositoryBase<SubCategoryEntity>, ISubCategoryRepository
    {
        public SubCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override SubCategoryEntity GetById(string subCategoryId)
        {
            return DbContext.SubCategory.Include("Category").FirstOrDefault(subcat => subcat.SubCategoryId == subCategoryId);
        }
    }
}
