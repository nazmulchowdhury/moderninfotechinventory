using Data.Infrastructure;
using Data.Helper;
using Model.Purchase;
using System.Linq;

namespace Data.Repositories.Purchase
{
    public class PurchaseEntryRepository : RepositoryBase<PurchaseEntryEntity>, IPurchaseEntryRepository
    {
        public PurchaseEntryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override PurchaseEntryEntity GetById(string purchaseEntryId)
        {
            return DbContext.PurchaseEntry.Include("Supplier").Include("ProductQuantity").Include("InvoiceInfo").FirstOrDefault(purent => purent.PurchaseEntryId == purchaseEntryId);
        }
    }
}
