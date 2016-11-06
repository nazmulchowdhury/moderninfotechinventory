using System.Collections.Generic;
using Model.InvoiceInfo;

namespace Service.InvoiceInfo
{
    public interface IInvoiceInfoServices
    {
        IEnumerable<InvoiceInfoEntity> GetAllInvoices();
        InvoiceInfoEntity GetInvoice(string invoiceInfoId);
        InvoiceInfoEntity CreateInvoice(InvoiceInfoEntity invoiceInfoEntity);
        bool UpdateInvoice(string invoiceInfoId, InvoiceInfoEntity invoiceInfoEntity);
        bool DeleteInvoice(string invoiceInfoId);
    }
}
