using Data.Infrastructure;
using Data.Helper;
using Model.Sale;
using System.Linq;

namespace Data.Repositories.Sale
{
    public class SaleReturnQuantityRepository : RepositoryBase<SaleReturnQuantityEntity>, ISaleReturnQuantityRepository
    {
        public SaleReturnQuantityRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override SaleReturnQuantityEntity GetById(string saleReturnQuantityId)
        {
            return DbContext.SaleReturnQuantity.Include("BillEntry").FirstOrDefault(salrtn => salrtn.SaleReturnQuantityId == saleReturnQuantityId);
        }
    }
}
