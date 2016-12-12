using System;
using System.Linq;
using Data.Helper;
using Model.Inventory;
using Model.Purchase;
using Model.InvoiceInfo;
using Data.Infrastructure;

namespace Data.Repositories.Purchase
{
    public class PurchaseReturnRepository : RepositoryBase<PurchaseReturnEntity>, IPurchaseReturnRepository
    {
        public PurchaseReturnRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override PurchaseReturnEntity Add(PurchaseReturnEntity purchaseReturnEntity)
        {
            var insertedEntity = DbContext.PurchaseReturn.Add(purchaseReturnEntity);
            DbContext.InvoiceInfo.Add(
                new InvoiceInfoEntity
                {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    PurchaseReturnId = insertedEntity.PurchaseReturnId,
                    Status = true
                });
            DbContext.Commit();
            return insertedEntity;
        }

        public override bool Delete(string purchaseReturnId)
        {
            var purchaseReturnEntity = DbContext.PurchaseReturn.Find(purchaseReturnId);

            if (purchaseReturnEntity != null)
            {
                var invoiceInfoEntity = DbContext.InvoiceInfo.FirstOrDefault(invinf => invinf.PurchaseReturnId == purchaseReturnId);
                if (invoiceInfoEntity != null)
                {
                    DbContext.InvoiceInfo.Remove(invoiceInfoEntity);
                }

                DbContext.Entry(purchaseReturnEntity).Collection("ProductReturnQuantities").Load();
                var productReturnQuantities = purchaseReturnEntity.ProductReturnQuantities.ToList();
                foreach (ProductReturnQuantityEntity productReturnQuantity in productReturnQuantities)
                {
                    purchaseReturnEntity.ProductReturnQuantities.Remove(productReturnQuantity);
                    DbContext.ProductReturnQuantity.Remove(productReturnQuantity);
                }

                DbContext.PurchaseReturn.Remove(purchaseReturnEntity);
                DbContext.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
