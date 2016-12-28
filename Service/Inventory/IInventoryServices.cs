using Model.Inventory;
using System.Collections.Generic;

namespace Service.Inventory
{
    public interface IInventoryServices
    {
        ICollection<InventoryEntity> GetAllInventories();
        InventoryEntity GetInventory(string inventoryId);
        InventoryEntity CreateInventory(InventoryEntity inventoryEntity);
        bool UpdateInventory(string inventoryId, InventoryEntity inventoryEntity);
        bool DeleteInventory(string inventoryId);
    }
}
