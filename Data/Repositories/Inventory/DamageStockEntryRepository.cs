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
            return DbContext.DamageStockEntry.Include("ProductQuantity").FirstOrDefault(dmgstk => dmgstk.DamageStockEntryId == damageStockEntryId);
        }

        public override bool Delete(string damageStockEntryId)
        {
            var damageStockEntryEntity = DbContext.DamageStockEntry.Find(damageStockEntryId);

            if (damageStockEntryEntity != null)
            {
                var productQuantityEntity = DbContext.ProductQuantity.FirstOrDefault(proqty => proqty.ProductQuantityId == damageStockEntryEntity.ProductQuantityId);
                if (productQuantityEntity != null)
                {
                    DbContext.ProductQuantity.Remove(productQuantityEntity);
                }

                DbContext.DamageStockEntry.Remove(damageStockEntryEntity);
                DbContext.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
