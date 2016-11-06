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

        public override InvoiceInfoEntity GetById(string InvoiceInfoId)
        {
            return DbContext.InvoiceInfo.Include("BillEntry").Include("PurchaseEntry").FirstOrDefault(invinf => invinf.InvoiceInfoId == InvoiceInfoId);
        }

        public bool DeleteByBillEntryId(string billEntryId)
        {
            var invoiceInfoEntity = DbContext.InvoiceInfo.FirstOrDefault(invinf => invinf.BillEntryId == billEntryId);
            if (invoiceInfoEntity != null)
            {
                return this.Delete(invoiceInfoEntity.InvoiceInfoId);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteByPurchaseEntryId(string purchaseEntryId)
        {
            var invoiceInfoEntity = DbContext.InvoiceInfo.FirstOrDefault(invinf => invinf.PurchaseEntryId == purchaseEntryId);
            if (invoiceInfoEntity != null)
            {
                return this.Delete(invoiceInfoEntity.InvoiceInfoId);
            }
            else
            {
                return false;
            }
        }
    }
}
