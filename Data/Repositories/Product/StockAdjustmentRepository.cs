using Data.Infrastructure;
using Data.Helper;
using Model.Product;
using System.Linq;

namespace Data.Repositories.Product
{
    public class StockAdjustmentRepository : RepositoryBase<StockAdjustmentEntity>, IStockAdjustmentRepository
    {
        public StockAdjustmentRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override StockAdjustmentEntity GetById(string stockAdjustmentId)
        {
            return DbContext.StockAdjustment.Include("ProductQuantity").FirstOrDefault(stkadj => stkadj.StockAdjustmentId == stockAdjustmentId);
        }
    }
}
