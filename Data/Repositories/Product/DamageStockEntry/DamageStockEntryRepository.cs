using Data.Infrastructure;
using Data.Helper;
using Model.Product;
using System.Linq;

namespace Data.Repositories.Product.DamageStockEntry
{
    public class DamageStockEntryRepository : RepositoryBase<DamageStockEntryEntity>, IDamageStockEntryRepository
    {
        public DamageStockEntryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override DamageStockEntryEntity GetById(string damageStockEntryId)
        {
            return DbContext.DamageStockEntry.Include("StockAdjustment").FirstOrDefault(dmgstk=> dmgstk.DamageStockEntryId == damageStockEntryId);
        }
    }
}
