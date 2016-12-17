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
            var stockAdjustmentEntity = Context.StockAdjustment.Find(stockAdjustmentId);

            if (stockAdjustmentEntity != null)
            {
                Context.Entry(stockAdjustmentEntity).Collection("ProductQuantities").Load();
                var productQuantities = stockAdjustmentEntity.ProductQuantities.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    stockAdjustmentEntity.ProductQuantities.Remove(productQuantity);
                    Context.ProductQuantity.Remove(productQuantity);
                }

                Context.StockAdjustment.Remove(stockAdjustmentEntity);
                Context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
