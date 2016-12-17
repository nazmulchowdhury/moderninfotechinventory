using System;
using System.Linq;
using Data.Helper;
using Model.Inventory;
using Model.Purchase;
using Model.InvoiceInfo;
using Model.Utilities;
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
            var insertedEntity = Context.PurchaseReturn.Add(purchaseReturnEntity);
            Context.InvoiceInfo.Add(
                new InvoiceInfoEntity
                {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    EntryId = insertedEntity.PurchaseReturnId,
                    EntryType = Option.PURCHASE_RETURN,
                    Status = true
                });
            Context.Commit();
            return insertedEntity;
        }

        public override bool Delete(string purchaseReturnId)
        {
            var purchaseReturnEntity = Context.PurchaseReturn.Find(purchaseReturnId);

            if (purchaseReturnEntity != null)
            {
                var invoiceInfoEntity = Context.InvoiceInfo.FirstOrDefault(invinf => invinf.EntryId == purchaseReturnId);
                if (invoiceInfoEntity != null)
                {
                    Context.InvoiceInfo.Remove(invoiceInfoEntity);
                }

                Context.Entry(purchaseReturnEntity).Collection("ProductReturnQuantities").Load();
                var productReturnQuantities = purchaseReturnEntity.ProductReturnQuantities.ToList();
                foreach (ProductReturnQuantityEntity productReturnQuantity in productReturnQuantities)
                {
                    purchaseReturnEntity.ProductReturnQuantities.Remove(productReturnQuantity);
                    Context.ProductReturnQuantity.Remove(productReturnQuantity);
                }

                Context.PurchaseReturn.Remove(purchaseReturnEntity);
                Context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
