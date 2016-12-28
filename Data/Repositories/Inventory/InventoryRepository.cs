using Data.Helper;
using System.Linq;
using Model.Inventory;
using Data.Infrastructure;

namespace Data.Repositories.Inventory
{
    public class InventoryRepository : RepositoryBase<InventoryEntity>, IInventoryRepository
    {
        public InventoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override InventoryEntity GetById(string inventoryId)
        {
            return Context.Inventory.Include("ProductQuantity").Include("TenantInfo").FirstOrDefault(inv => inv.InventoryId == inventoryId);
        }

        public override bool Delete(string inventoryId)
        {
            var inventoryEntity = Context.Inventory.Find(inventoryId);
            if (inventoryEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(inventoryEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Inventory.Remove(inventoryEntity);
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
