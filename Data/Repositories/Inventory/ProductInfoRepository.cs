using Data.Infrastructure;
using Data.Helper;
using Model.Inventory;
using System.Linq;

namespace Data.Repositories.Inventory
{
    public class ProductInfoRepository : RepositoryBase<ProductInfoEntity>, IProductInfoRepository
    {
        public ProductInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override ProductInfoEntity GetById(string productId)
        {
            return DbContext.ProductInfo.Include("SubCategory").FirstOrDefault(pro => pro.ProductId == productId);
        }
    }
}
