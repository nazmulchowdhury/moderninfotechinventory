﻿using System.Collections.Generic;
using Data.Repositories.InvoiceInfo;
using Model.InvoiceInfo;

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

        public bool UpdateInvoice(string invoiceInfoId, InvoiceInfoEntity invoiceInfoEntity)
        {
            var storedItem = invoiceInfoRepository.GetById(invoiceInfoId);

            if (storedItem != null)
            {
                storedItem.BillEntryId = invoiceInfoEntity.BillEntryId;
                storedItem.PurchaseEntryId = invoiceInfoEntity.PurchaseEntryId;

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
