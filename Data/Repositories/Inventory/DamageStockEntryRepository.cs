using Data.Infrastructure;
using Data.Helper;
using Model.Inventory;
using System.Linq;

namespace Data.Repositories.Inventory
{
    public class DamageStockEntryRepository : RepositoryBase<DamageStockEntryEntity>, IDamageStockEntryRepository
    {
        public DamageStockEntryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override DamageStockEntryEntity GetById(string damageStockEntryId)
        {
            return Context.DamageStockEntry.Include("ProductQuantity").FirstOrDefault(dmgstk => dmgstk.DamageStockEntryId == damageStockEntryId);
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
