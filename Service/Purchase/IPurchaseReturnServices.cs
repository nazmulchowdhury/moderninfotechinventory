using Model.Purchase;
using System.Collections.Generic;

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
