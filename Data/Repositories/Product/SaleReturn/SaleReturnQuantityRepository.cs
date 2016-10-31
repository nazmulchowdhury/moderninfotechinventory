using Data.Infrastructure;
using Data.Helper;
using Model.Product;
using System.Linq;

namespace Data.Repositories.Product.SaleReturn
{
    public class SaleReturnQuantityRepository : RepositoryBase<SaleReturnQuantityEntity>, ISaleReturnQuantityRepository
    {
        public SaleReturnQuantityRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override SaleReturnQuantityEntity GetById(string saleReturnQuantityId)
        {
            return DbContext.SaleReturnQuantity.Include("ProductQuantity").FirstOrDefault(salrtn => salrtn.SaleReturnQuantityId == saleReturnQuantityId);
        }
    }
}
