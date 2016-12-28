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

        public override PurchaseReturnEntity GetById(string purchaseReturnId)
        {
            return Context.PurchaseReturn.Include("TenantInfo").FirstOrDefault(purrtn => purrtn.PurchaseReturnId == purchaseReturnId);
        }

        public override PurchaseReturnEntity Add(PurchaseReturnEntity purchaseReturnEntity)
        {
            var insertedEntity = Context.PurchaseReturn.Add(purchaseReturnEntity);
            Context.InvoiceInfo.Add(new InvoiceInfoEntity
            {
                InvoiceInfoId = Guid.NewGuid().ToString(),
                EntryId = insertedEntity.PurchaseReturnId,
                EntryType = Option.PURCHASE_RETURN,
                TenantId = purchaseReturnEntity.TenantId
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

                Context.Entry(purchaseReturnEntity).Collection("PurchaseReturnedProducts").Load();
                var productReturnQuantities = purchaseReturnEntity.PurchaseReturnedProducts.ToList();
                foreach (ProductReturnQuantityEntity productReturnQuantity in productReturnQuantities)
                {
                    purchaseReturnEntity.PurchaseReturnedProducts.Remove(productReturnQuantity);
                    Context.ProductReturnQuantity.Remove(productReturnQuantity);
                }

                var tenantEntity = Context.Tenant.Find(purchaseReturnEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
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
