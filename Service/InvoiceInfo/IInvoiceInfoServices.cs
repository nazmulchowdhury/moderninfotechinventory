using Model.InvoiceInfo;
using System.Collections.Generic;

namespace Service.InvoiceInfo
{
    public interface IInvoiceInfoServices
    {
        ICollection<InvoiceInfoEntity> GetAllInvoices();
        InvoiceInfoEntity GetInvoice(string invoiceInfoId);
        InvoiceInfoEntity CreateInvoice(InvoiceInfoEntity invoiceInfoEntity);
        bool UpdateInvoice(string invoiceInfoId, InvoiceInfoEntity invoiceInfoEntity);
        bool DeleteInvoice(string invoiceInfoId);
    }
}
