using Data.Infrastructure;
using Data.Helper;
using Model.Product;
using System.Linq;

namespace Data.Repositories.Product.ProductInfo
{
    public class ProductQuantityRepository : RepositoryBase<ProductQuantityEntity>, IProductQuantityRepository
    {
        public ProductQuantityRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override ProductQuantityEntity GetById(string productQuantityId)
        {
            return DbContext.ProductQuantity.Include("Product").Include("Customer").FirstOrDefault(proqty => proqty.ProductQuantityId == productQuantityId);
        }
    }
}
