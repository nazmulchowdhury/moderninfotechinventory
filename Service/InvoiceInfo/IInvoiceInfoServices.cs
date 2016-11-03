using System.Collections.Generic;
using Model.Invoice;

namespace Service.InvoiceInfo
{
    public interface IInvoiceInfoServices
    {
        IEnumerable<InvoiceInfoEntity> GetAllInvoices();
        InvoiceInfoEntity GetInvoice(string invoiceInfoId);
        InvoiceInfoEntity CreateInvoice(InvoiceInfoEntity invoiceInfoEntity);
        bool DeleteInvoice(string invoiceInfoId);
    }
}
