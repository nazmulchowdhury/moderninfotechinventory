using Data.Helper;
using System.Linq;
using Model.Inventory;
using Data.Infrastructure;

namespace Data.Repositories.Inventory
{
    public class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override CategoryEntity GetById(string categoryId)
        {
            return Context.Category.Include("TenantInfo").FirstOrDefault(cat => cat.CategoryId == categoryId);
        }

        public override bool Delete(string categoryId)
        {
            var categoryEntity = Context.Category.Find(categoryId);
            if (categoryEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(categoryEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Category.Remove(categoryEntity);
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
