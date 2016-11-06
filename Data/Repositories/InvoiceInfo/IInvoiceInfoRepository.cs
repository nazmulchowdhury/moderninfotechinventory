using Data.Infrastructure;
using Model.InvoiceInfo;

namespace Data.Repositories.InvoiceInfo
{
    public interface IInvoiceInfoRepository : IRepository<InvoiceInfoEntity>
    {
        bool DeleteByBillEntryId(string billEntryId);
        bool DeleteByPurchaseEntryId(string purchaseEntryId);
    }
}
