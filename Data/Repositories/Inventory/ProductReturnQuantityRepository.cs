using Data.Helper;
using System.Linq;
using Model.Inventory;
using Data.Infrastructure;

namespace Data.Repositories.Inventory
{
    public class ProductReturnQuantityRepository : RepositoryBase<ProductReturnQuantityEntity>, IProductReturnQuantityRepository
    {
        public ProductReturnQuantityRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override ProductReturnQuantityEntity GetById(string productReturnQuantityId)
        {
            return Context.ProductReturnQuantity.Include("ProductQuantity").Include("TenantInfo").FirstOrDefault(prortnqty => prortnqty.ProductReturnQuantityId == productReturnQuantityId);
        }

        public override bool Delete(string productReturnQuantityId)
        {
            var productReturnQuantityEntity = Context.ProductReturnQuantity.Find(productReturnQuantityId);
            if (productReturnQuantityEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(productReturnQuantityEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.ProductReturnQuantity.Remove(productReturnQuantityEntity);
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
