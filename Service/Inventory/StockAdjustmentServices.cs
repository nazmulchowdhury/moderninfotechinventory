using Model.Inventory;
using System.Collections.Generic;
using Data.Repositories.Inventory;

namespace Service.Inventory
{
    public class StockAdjustmentServices : IStockAdjustmentServices
    {
        private readonly IStockAdjustmentRepository stockAdjustmentRepository;

        public StockAdjustmentServices(IStockAdjustmentRepository stockAdjustmentRepository)
        {
            this.stockAdjustmentRepository = stockAdjustmentRepository;
        }

        public ICollection<StockAdjustmentEntity> GetAllStockAdjustments()
        {
            return stockAdjustmentRepository.GetAll();
        }

        public StockAdjustmentEntity GetStockAdjustment(string stockAdjustmentId)
        {
            return stockAdjustmentRepository.GetById(stockAdjustmentId);
        }

        public StockAdjustmentEntity CreateStockAdjustment(StockAdjustmentEntity stockAdjustmentEntity)
        {
            return stockAdjustmentRepository.Add(stockAdjustmentEntity);
        }

        public bool UpdateStockAdjustment(string stockAdjustmentId, StockAdjustmentEntity stockAdjustmentEntity)
        {
            var storedItem = stockAdjustmentRepository.GetById(stockAdjustmentId);

            if (storedItem != null)
            {
                storedItem.ReceiveDate = stockAdjustmentEntity.ReceiveDate;
                storedItem.ReceiveNumber = stockAdjustmentEntity.ReceiveNumber;
                storedItem.TenantInfo.UserId = stockAdjustmentEntity.TenantInfo.UserId;

                stockAdjustmentRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteStockAdjustment(string stockAdjustmentId)
        {
            return stockAdjustmentRepository.Delete(stockAdjustmentId);
        }
    }
}
