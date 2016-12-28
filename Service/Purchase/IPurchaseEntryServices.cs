using Model.Purchase;
using System.Collections.Generic;

namespace Service.Purchase
{
    public interface IPurchaseEntryServices
    {
        ICollection<PurchaseEntryEntity> GetAllPurchaseEntries();
        PurchaseEntryEntity GetPurchaseEntry(string purchaseEntryId);
        PurchaseEntryEntity CreatePurchaseEntry(PurchaseEntryEntity purchaseEntryEntity);
        bool UpdatePurchaseEntry(string purchaseEntryId, PurchaseEntryEntity purchaseEntryEntity);
        bool DeletePurchaseEntry(string purchaseEntryId);
    }
}
