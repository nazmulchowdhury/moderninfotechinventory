using Data.Infrastructure;
using Data.Helper;
using Model.Invoice;
using System.Linq;

namespace Data.Repositories.InvoiceInfo
{
    public class InvoiceInfoRepository : RepositoryBase<InvoiceInfoEntity>, IInvoiceInfoRepository
    {
        public InvoiceInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InvoiceInfoEntity GetById(string invoiceInfoId)
        {
            return DbContext.InvoiceInfo.Include("Customer").FirstOrDefault(inv => inv.InvoiceInfoId == invoiceInfoId);
        }
    }
}
