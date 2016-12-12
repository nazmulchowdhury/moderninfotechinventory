using System.Collections.Generic;
using Model.Purchase;

namespace Service.Purchase
{
    public interface IPurchaseReturnServices
    {
        ICollection<PurchaseReturnEntity> GetAllPurchaseReturns();
        PurchaseReturnEntity GetPurchaseReturn(string purchaseReturnId);
        PurchaseReturnEntity CreatePurchaseReturn(PurchaseReturnEntity purchaseReturnEntity);
        bool UpdatePurchaseReturn(string purchaseReturnId, PurchaseReturnEntity purchaseReturnEntity);
        bool DeletePurchaseReturn(string purchaseReturnId);
    }
}
