using Data.Infrastructure;
using Data.Helper;
using Model.InvoiceInfo;
using System.Linq;

namespace Data.Repositories.InvoiceInfo
{
    public class InvoiceInfoRepository : RepositoryBase<InvoiceInfoEntity>, IInvoiceInfoRepository
    {
        public InvoiceInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InvoiceInfoEntity GetById(string invoiceInfoId)
        {
            return DbContext.InvoiceInfo.Include("BillEntry").Include("PurchaseEntry").Include("SaleReturn").Include("PurchaseReturn").FirstOrDefault(invinf => invinf.InvoiceInfoId == invoiceInfoId);
        }
    }
}
