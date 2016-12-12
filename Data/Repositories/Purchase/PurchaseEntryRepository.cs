using Data.Infrastructure;
using Data.Helper;
using Model.Purchase;
using Model.Inventory;
using Model.InvoiceInfo;
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
            var insertedEntity = DbContext.PurchaseEntry.Add(purchaseEntryEntity);
            DbContext.InvoiceInfo.Add(
                new InvoiceInfoEntity
                {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    PurchaseEntryId = insertedEntity.PurchaseEntryId,
                    Status = true
                });
            DbContext.Commit();
            return insertedEntity;
        }

        public override PurchaseEntryEntity GetById(string purchaseEntryId)
        {
            return DbContext.PurchaseEntry.Include("Supplier").FirstOrDefault(purent => purent.PurchaseEntryId == purchaseEntryId);
        }

        public override bool Delete(string purchaseEntryId)
        {
            var purchaseEntryEntity = DbContext.PurchaseEntry.Find(purchaseEntryId);

            if (purchaseEntryEntity != null)
            {
                var invoiceInfoEntity = DbContext.InvoiceInfo.FirstOrDefault(invinf => invinf.PurchaseEntryId == purchaseEntryId);
                if (invoiceInfoEntity != null)
                {
                    DbContext.InvoiceInfo.Remove(invoiceInfoEntity);

                    var purchaseReturnEntities = DbContext.PurchaseReturn.Where(purrtn => purrtn.RefInvoiceId == invoiceInfoEntity.InvoiceInfoId);
                    foreach (PurchaseReturnEntity purchaseReturnEntity in purchaseReturnEntities)
                    {
                        invoiceInfoEntity = DbContext.InvoiceInfo.FirstOrDefault(invinf => invinf.PurchaseReturnId == purchaseReturnEntity.PurchaseReturnId);
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
                    }
                }

                DbContext.Entry(purchaseEntryEntity).Collection("ProductQuantities").Load();
                var productQuantities = purchaseEntryEntity.ProductQuantities.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    purchaseEntryEntity.ProductQuantities.Remove(productQuantity);
                    DbContext.ProductQuantity.Remove(productQuantity);
                }

                DbContext.PurchaseEntry.Remove(purchaseEntryEntity);
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
