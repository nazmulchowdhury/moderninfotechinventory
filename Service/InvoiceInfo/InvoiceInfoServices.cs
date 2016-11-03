using System.Collections.Generic;
using Data.Repositories.InvoiceInfo;
using Model.Invoice;

namespace Service.InvoiceInfo
{
    public class InvoiceInfoServices : IInvoiceInfoServices
    {
        private readonly IInvoiceInfoRepository invoiceInfoRepository;

        public InvoiceInfoServices(IInvoiceInfoRepository invoiceInfoRepository)
        {
            this.invoiceInfoRepository = invoiceInfoRepository;
        }

        public IEnumerable<InvoiceInfoEntity> GetAllInvoices()
        {
            return invoiceInfoRepository.GetAll();
        }

        public InvoiceInfoEntity GetInvoice(string invoiceInfoId)
        {
            return invoiceInfoRepository.GetById(invoiceInfoId);
        }

        public InvoiceInfoEntity CreateInvoice(InvoiceInfoEntity invoiceInfoEntity)
        {
            return invoiceInfoRepository.Add(invoiceInfoEntity);
        }

        public bool DeleteInvoice(string invoiceInfoId)
        {
            return invoiceInfoRepository.Delete(invoiceInfoId);
        }
    }
}
