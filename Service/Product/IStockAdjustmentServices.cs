using System.Collections.Generic;
using Model.Product;

namespace Service.Product
{
    public interface IStockAdjustmentServices
    {
        IEnumerable<StockAdjustmentEntity> GetAllStockAdjustments();
        StockAdjustmentEntity GetStockAdjustment(string stockAdjustmentId);
        StockAdjustmentEntity CreateStockAdjustment(StockAdjustmentEntity stockAdjustmentEntity);
        bool UpdateStockAdjustment(string stockAdjustmentId, StockAdjustmentEntity stockAdjustmentEntity);
        bool DeleteStockAdjustment(string stockAdjustmentId);
    }
}
