using Data.Infrastructure;
using Data.Helper;
using Model.Inventory;
using System.Linq;

namespace Data.Repositories.Inventory
{
    public class SubCategoryRepository : RepositoryBase<SubCategoryEntity>, ISubCategoryRepository
    {
        public SubCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override SubCategoryEntity GetById(string subCategoryId)
        {
            return Context.SubCategory.Include("Category").Include("Unit").FirstOrDefault(subcat => subcat.SubCategoryId == subCategoryId);
        }
    }
}
