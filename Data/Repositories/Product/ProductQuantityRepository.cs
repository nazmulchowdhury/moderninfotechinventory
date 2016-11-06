using Data.Infrastructure;
using Data.Helper;
using Model.Product;
using System.Linq;

namespace Data.Repositories.Product
{
    public class ProductQuantityRepository : RepositoryBase<ProductQuantityEntity>, IProductQuantityRepository
    {
        public ProductQuantityRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override ProductQuantityEntity GetById(string productQuantityId)
        {
            return DbContext.ProductQuantity.Include("Product").FirstOrDefault(proqty => proqty.ProductQuantityId == productQuantityId);
        }
    }
}
