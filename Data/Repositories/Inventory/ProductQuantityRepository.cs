using Data.Helper;
using System.Linq;
using Model.Inventory;
using Data.Infrastructure;

namespace Data.Repositories.Inventory
{
    public class ProductQuantityRepository : RepositoryBase<ProductQuantityEntity>, IProductQuantityRepository
    {
        public ProductQuantityRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override ProductQuantityEntity GetById(string productQuantityId)
        {
            return Context.ProductQuantity.Include("Product").Include("TenantInfo").FirstOrDefault(proqty => proqty.ProductQuantityId == productQuantityId);
        }

        public override bool Delete(string productQuantityId)
        {
            var productQuantityEntity = Context.ProductQuantity.Find(productQuantityId);
            if (productQuantityEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(productQuantityEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.ProductQuantity.Remove(productQuantityEntity);
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
