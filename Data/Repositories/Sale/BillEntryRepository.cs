using Data.Infrastructure;
using Data.Helper;
using Model.Sale;
using System.Linq;

namespace Data.Repositories.Sale
{
    public class BillEntryRepository : RepositoryBase<BillEntryEntity>, IBillEntryRepository
    {
        public BillEntryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override BillEntryEntity GetById(string billEntryId)
        {
            return DbContext.BillEntry.Include("ProductQuantity").Include("Customer").FirstOrDefault(bill => bill.BillEntryId == billEntryId);
        }
    }
}
