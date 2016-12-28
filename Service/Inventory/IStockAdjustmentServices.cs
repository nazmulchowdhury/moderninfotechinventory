using Model.Inventory;
using System.Collections.Generic;

namespace Service.Inventory
{
    public interface IStockAdjustmentServices
    {
        ICollection<StockAdjustmentEntity> GetAllStockAdjustments();
        StockAdjustmentEntity GetStockAdjustment(string stockAdjustmentId);
        StockAdjustmentEntity CreateStockAdjustment(StockAdjustmentEntity stockAdjustmentEntity);
        bool UpdateStockAdjustment(string stockAdjustmentId, StockAdjustmentEntity stockAdjustmentEntity);
        bool DeleteStockAdjustment(string stockAdjustmentId);
    }
}
