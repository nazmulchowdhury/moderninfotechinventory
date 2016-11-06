using System.Collections.Generic;
using Data.Repositories.Purchase;
using Data.Repositories.InvoiceInfo;
using Model.Purchase;
using Model.InvoiceInfo;
using System;

namespace Service.Purchase
{
    public class PurchaseEntryServices : IPurchaseEntryServices
    {
        private readonly IPurchaseEntryRepository purchaseEntryRepository;
        private readonly IInvoiceInfoRepository invoiceInfoRepository;

        public PurchaseEntryServices(IPurchaseEntryRepository purchaseEntryRepository, IInvoiceInfoRepository invoiceInfoRepository)
        {
            this.purchaseEntryRepository = purchaseEntryRepository;
            this.invoiceInfoRepository = invoiceInfoRepository;
        }

        public IEnumerable<PurchaseEntryEntity> GetAllPurchaseEntries()
        {
            return purchaseEntryRepository.GetAll();
        }

        public PurchaseEntryEntity GetPurchaseEntry(string purchaseEntryId)
        {
            return purchaseEntryRepository.GetById(purchaseEntryId);
        }

        public PurchaseEntryEntity CreatePurchaseEntry(PurchaseEntryEntity purchaseEntryEntity)
        {
            var insertedEntity = purchaseEntryRepository.Add(purchaseEntryEntity);
            invoiceInfoRepository.Add(
                new InvoiceInfoEntity
                {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    PurchaseEntryId = insertedEntity.PurchaseEntryId
                });
            return insertedEntity;
        }

        public bool UpdatePurchaseEntry(string purchaseEntryId, PurchaseEntryEntity purchaseEntryEntity)
        {
            var storedItem = purchaseEntryRepository.GetById(purchaseEntryId);

            if (storedItem != null)
            {
                storedItem.SupplierId = purchaseEntryEntity.SupplierId;
                storedItem.ProductQuantityId = purchaseEntryEntity.ProductQuantityId;
                storedItem.ReceiveDate = purchaseEntryEntity.ReceiveDate;
                storedItem.ReceiveNumber = purchaseEntryEntity.ReceiveNumber;

                purchaseEntryRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletePurchaseEntry(string purchaseEntryId)
        {
            var isDeletedInvoiceInfo = invoiceInfoRepository.DeleteByPurchaseEntryId(purchaseEntryId);
            if (isDeletedInvoiceInfo)
            {
                return purchaseEntryRepository.Delete(purchaseEntryId);
            }
            else
            {
                return false;
            }
        }
    }
}
