using Data.Infrastructure;
using Data.Helper;
using Model.Inventory;
using System.Linq;

namespace Data.Repositories.Inventory
{
    public class InventoryRepository : RepositoryBase<InventoryEntity>, IInventoryRepository
    {
        public InventoryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InventoryEntity GetById(string inventoryId)
        {
            return DbContext.Inventory.Include("ProductQuantity").FirstOrDefault(inv => inv.InventoryId == inventoryId);
        }
    }
}
