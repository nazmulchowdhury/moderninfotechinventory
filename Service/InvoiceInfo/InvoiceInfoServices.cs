using Model.InvoiceInfo;
using System.Collections.Generic;
using Data.Repositories.InvoiceInfo;

namespace Service.InvoiceInfo
{
    public class InvoiceInfoServices : IInvoiceInfoServices
    {
        private readonly IInvoiceInfoRepository invoiceInfoRepository;

        public InvoiceInfoServices(IInvoiceInfoRepository invoiceInfoRepository)
        {
            this.invoiceInfoRepository = invoiceInfoRepository;
        }

        public ICollection<InvoiceInfoEntity> GetAllInvoices()
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

        public bool UpdateInvoice(string invoiceInfoId, InvoiceInfoEntity invoiceInfoEntity)
        {
            var storedItem = invoiceInfoRepository.GetById(invoiceInfoId);

            if (storedItem != null)
            {
                storedItem.EntryId = invoiceInfoEntity.EntryId;
                storedItem.EntryType = invoiceInfoEntity.EntryType;
                storedItem.TenantInfo.UserId = invoiceInfoEntity.TenantInfo.UserId;

                invoiceInfoRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }


        public bool DeleteInvoice(string invoiceInfoId)
        {
            return invoiceInfoRepository.Delete(invoiceInfoId);
        }
    }
}
