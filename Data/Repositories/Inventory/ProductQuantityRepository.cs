using Data.Infrastructure;
using Data.Helper;
using Model.Inventory;
using System.Linq;

namespace Data.Repositories.Inventory
{
    public class ProductQuantityRepository : RepositoryBase<ProductQuantityEntity>, IProductQuantityRepository
    {
        public ProductQuantityRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override ProductQuantityEntity GetById(string productQuantityId)
        {
            return Context.ProductQuantity.Include("Product").FirstOrDefault(proqty => proqty.ProductQuantityId == productQuantityId);
        }
    }
}
