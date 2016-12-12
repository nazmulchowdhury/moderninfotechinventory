using System.Collections.Generic;
using Data.Repositories.Purchase;
using Model.Purchase;

namespace Service.Purchase
{
    public class PurchaseEntryServices : IPurchaseEntryServices
    {
        private readonly IPurchaseEntryRepository purchaseEntryRepository;

        public PurchaseEntryServices(IPurchaseEntryRepository purchaseEntryRepository)
        {
            this.purchaseEntryRepository = purchaseEntryRepository;
        }

        public ICollection<PurchaseEntryEntity> GetAllPurchaseEntries()
        {
            return purchaseEntryRepository.GetAll();
        }

        public PurchaseEntryEntity GetPurchaseEntry(string purchaseEntryId)
        {
            return purchaseEntryRepository.GetById(purchaseEntryId);
        }

        public PurchaseEntryEntity CreatePurchaseEntry(PurchaseEntryEntity purchaseEntryEntity)
        {
            return purchaseEntryRepository.Add(purchaseEntryEntity);
        }

        public bool UpdatePurchaseEntry(string purchaseEntryId, PurchaseEntryEntity purchaseEntryEntity)
        {
            var storedItem = purchaseEntryRepository.GetById(purchaseEntryId);

            if (storedItem != null)
            {
                storedItem.SupplierId = purchaseEntryEntity.SupplierId;
                storedItem.ReceiveDate = purchaseEntryEntity.ReceiveDate;
                storedItem.ReceiveNumber = purchaseEntryEntity.ReceiveNumber;
                storedItem.PaidAmount = purchaseEntryEntity.PaidAmount;

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
            return purchaseEntryRepository.Delete(purchaseEntryId);
        }
    }
}
