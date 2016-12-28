using Data.Helper;
using System.Linq;
using Model.Inventory;
using Data.Infrastructure;

namespace Data.Repositories.Inventory
{
    public class SubCategoryRepository : RepositoryBase<SubCategoryEntity>, ISubCategoryRepository
    {
        public SubCategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override SubCategoryEntity GetById(string subCategoryId)
        {
            return Context.SubCategory.Include("Category").Include("Unit").Include("TenantInfo").FirstOrDefault(subcat => subcat.SubCategoryId == subCategoryId);
        }

        public override bool Delete(string subCategoryId)
        {
            var subCategoryEntity = Context.SubCategory.Find(subCategoryId);
            if (subCategoryEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(subCategoryEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.SubCategory.Remove(subCategoryEntity);
                Context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
