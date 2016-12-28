using System.Linq;
using Data.Helper;
using Model.Inventory;
using Data.Infrastructure;

namespace Data.Repositories.Inventory
{
    public class ProductInfoRepository : RepositoryBase<ProductInfoEntity>, IProductInfoRepository
    {
        public ProductInfoRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override ProductInfoEntity GetById(string productId)
        {
            return Context.ProductInfo.Include("SubCategory").Include("TenantInfo").FirstOrDefault(pro => pro.ProductId == productId);
        }

        public override bool Delete(string productId)
        {
            var productEntity = Context.ProductInfo.Find(productId);
            if (productEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(productEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.ProductInfo.Remove(productEntity);
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
