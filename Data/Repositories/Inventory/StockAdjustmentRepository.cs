using Data.Infrastructure;
using Data.Helper;
using Model.Inventory;
using System.Linq;

namespace Data.Repositories.Inventory
{
    public class StockAdjustmentRepository : RepositoryBase<StockAdjustmentEntity>, IStockAdjustmentRepository
    {
        public StockAdjustmentRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override bool Delete(string stockAdjustmentId)
        {
            var stockAdjustmentEntity = DbContext.StockAdjustment.Find(stockAdjustmentId);

            if (stockAdjustmentEntity != null)
            {
                DbContext.Entry(stockAdjustmentEntity).Collection("ProductQuantities").Load();
                var productQuantities = stockAdjustmentEntity.ProductQuantities.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    stockAdjustmentEntity.ProductQuantities.Remove(productQuantity);
                    DbContext.ProductQuantity.Remove(productQuantity);
                }

                DbContext.StockAdjustment.Remove(stockAdjustmentEntity);
                DbContext.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
