using Data.Helper;
using System.Linq;
using Model.Inventory;
using Data.Infrastructure;

namespace Data.Repositories.Inventory
{
    public class StockAdjustmentRepository : RepositoryBase<StockAdjustmentEntity>, IStockAdjustmentRepository
    {
        public StockAdjustmentRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override StockAdjustmentEntity GetById(string stockAdjustmentId)
        {
            return Context.StockAdjustment.Include("TenantInfo").FirstOrDefault(stkadj => stkadj.StockAdjustmentId == stockAdjustmentId);
        }

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

                var tenantEntity = Context.Tenant.Find(stockAdjustmentEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
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
