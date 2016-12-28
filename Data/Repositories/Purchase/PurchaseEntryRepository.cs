using System;
using System.Linq;
using Data.Helper;
using Model.Purchase;
using Model.Inventory;
using Model.Utilities;
using Model.InvoiceInfo;
using Data.Infrastructure;

namespace Data.Repositories.Purchase
{
    public class PurchaseEntryRepository : RepositoryBase<PurchaseEntryEntity>, IPurchaseEntryRepository
    {
        public PurchaseEntryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override PurchaseEntryEntity Add(PurchaseEntryEntity purchaseEntryEntity)
        {
            var insertedEntity = Context.PurchaseEntry.Add(purchaseEntryEntity);
            Context.InvoiceInfo.Add(new InvoiceInfoEntity
            {
                InvoiceInfoId = Guid.NewGuid().ToString(),
                EntryId = insertedEntity.PurchaseEntryId,
                EntryType = Option.PURCHASE_ENTRY,
                TenantId = purchaseEntryEntity.TenantId
            });
            Context.Commit();
            return insertedEntity;
        }

        public override PurchaseEntryEntity GetById(string purchaseEntryId)
        {
            return Context.PurchaseEntry.Include("Supplier").Include("TenantInfo").FirstOrDefault(purent => purent.PurchaseEntryId == purchaseEntryId);
        }

        public override bool Delete(string purchaseEntryId)
        {
            var purchaseEntryEntity = Context.PurchaseEntry.Find(purchaseEntryId);

            if (purchaseEntryEntity != null)
            {
                var invoiceInfoEntity = Context.InvoiceInfo.FirstOrDefault(invinf => invinf.EntryId == purchaseEntryId);
                if (invoiceInfoEntity != null)
                {
                    Context.InvoiceInfo.Remove(invoiceInfoEntity);

                    var purchaseReturnEntities = Context.PurchaseReturn.Where(purrtn => purrtn.RefInvoiceId == invoiceInfoEntity.InvoiceInfoId);
                    foreach (PurchaseReturnEntity purchaseReturnEntity in purchaseReturnEntities)
                    {
                        invoiceInfoEntity = Context.InvoiceInfo.FirstOrDefault(invinf => invinf.EntryId == purchaseReturnEntity.PurchaseReturnId);
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
                        Context.PurchaseReturn.Remove(purchaseReturnEntity);
                    }
                }

                Context.Entry(purchaseEntryEntity).Collection("PurchasedProducts").Load();
                var productQuantities = purchaseEntryEntity.PurchasedProducts.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    purchaseEntryEntity.PurchasedProducts.Remove(productQuantity);
                    Context.ProductQuantity.Remove(productQuantity);
                }

                var tenantEntity = Context.Tenant.Find(purchaseEntryEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }

                Context.PurchaseEntry.Remove(purchaseEntryEntity);
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
