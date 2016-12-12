using Data.Infrastructure;
using Data.Helper;
using Model.Inventory;
using System.Linq;

namespace Data.Repositories.Inventory
{
    public class ProductReturnQuantityRepository : RepositoryBase<ProductReturnQuantityEntity>, IProductReturnQuantityRepository
    {
        public ProductReturnQuantityRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override ProductReturnQuantityEntity GetById(string productReturnQuantityId)
        {
            return DbContext.ProductReturnQuantity.Include("ProductQuantity").FirstOrDefault(prortnqty => prortnqty.ProductReturnQuantityId == productReturnQuantityId);
        }
    }
}
