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
            return Context.ProductInfo.Include("SubCategory").FirstOrDefault(pro => pro.ProductId == productId);
        }
    }
}
