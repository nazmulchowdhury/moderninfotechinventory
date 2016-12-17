using Data.Infrastructure;
using Data.Helper;
using Model.Purchase;
using Model.Inventory;
using Model.InvoiceInfo;
using Model.Utilities;
using System.Linq;
using System;

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
            Context.InvoiceInfo.Add(
                new InvoiceInfoEntity
                {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    EntryId = insertedEntity.PurchaseEntryId,
                    EntryType = Option.PURCHASE_ENTRY,
                    Status = true
                });
            Context.Commit();
            return insertedEntity;
        }

        public override PurchaseEntryEntity GetById(string purchaseEntryId)
        {
            return Context.PurchaseEntry.Include("Supplier").FirstOrDefault(purent => purent.PurchaseEntryId == purchaseEntryId);
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
                        Context.Entry(purchaseReturnEntity).Collection("ProductReturnQuantities").Load();
                        var productReturnQuantities = purchaseReturnEntity.ProductReturnQuantities.ToList();
                        foreach (ProductReturnQuantityEntity productReturnQuantity in productReturnQuantities)
                        {
                            purchaseReturnEntity.ProductReturnQuantities.Remove(productReturnQuantity);
                            Context.ProductReturnQuantity.Remove(productReturnQuantity);
                        }
                        Context.PurchaseReturn.Remove(purchaseReturnEntity);
                    }
                }

                Context.Entry(purchaseEntryEntity).Collection("ProductQuantities").Load();
                var productQuantities = purchaseEntryEntity.ProductQuantities.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    purchaseEntryEntity.ProductQuantities.Remove(productQuantity);
                    Context.ProductQuantity.Remove(productQuantity);
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
