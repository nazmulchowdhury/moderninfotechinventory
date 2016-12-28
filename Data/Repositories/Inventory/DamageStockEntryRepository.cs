using Data.Helper;
using System.Linq;
using Model.Inventory;
using Data.Infrastructure;

namespace Data.Repositories.Inventory
{
    public class DamageStockEntryRepository : RepositoryBase<DamageStockEntryEntity>, IDamageStockEntryRepository
    {
        public DamageStockEntryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override DamageStockEntryEntity GetById(string damageStockEntryId)
        {
            return Context.DamageStockEntry.Include("ProductQuantity").Include("TenantInfo").FirstOrDefault(dmgstk => dmgstk.DamageStockEntryId == damageStockEntryId);
        }

        public override bool Delete(string damageStockEntryId)
        {
            var damageStockEntryEntity = Context.DamageStockEntry.Find(damageStockEntryId);

            if (damageStockEntryEntity != null)
            {
                var productQuantityEntity = Context.ProductQuantity.FirstOrDefault(proqty => proqty.ProductQuantityId == damageStockEntryEntity.ProductQuantityId);
                if (productQuantityEntity != null)
                {
                    Context.ProductQuantity.Remove(productQuantityEntity);
                }

                var tenantEntity = Context.Tenant.Find(damageStockEntryEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }

                Context.DamageStockEntry.Remove(damageStockEntryEntity);
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
