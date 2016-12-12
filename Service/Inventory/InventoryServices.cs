using System.Collections.Generic;
using Data.Repositories.Inventory;
using Model.Inventory;

namespace Service.Inventory
{
    public class InventoryServices : IInventoryServices
    {
        private readonly IInventoryRepository inventoryRepository;

        public InventoryServices(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public ICollection<InventoryEntity> GetAllInventories()
        {
            return inventoryRepository.GetAll();
        }

        public InventoryEntity GetInventory(string inventoryId)
        {
            return inventoryRepository.GetById(inventoryId);
        }

        public InventoryEntity CreateInventory(InventoryEntity inventoryEntity)
        {
            return inventoryRepository.Add(inventoryEntity);
        }

        public bool UpdateInventory(string inventoryId, InventoryEntity inventoryEntity)
        {
            var storedItem = inventoryRepository.GetById(inventoryId);

            if (storedItem != null)
            {
                storedItem.ProductQuantityId = inventoryEntity.ProductQuantityId;
                storedItem.ReceiveDate = inventoryEntity.ReceiveDate;
                storedItem.ReceiveNumber = inventoryEntity.ReceiveNumber;

                inventoryRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteInventory(string inventoryId)
        {
            return inventoryRepository.Delete(inventoryId);
        }
    }
}
