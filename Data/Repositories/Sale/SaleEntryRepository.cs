using System;
using Model.Sale;
using System.Linq;
using Data.Helper;
using Model.Utilities;
using Model.Inventory;
using Model.InvoiceInfo;
using Data.Infrastructure;

namespace Data.Repositories.Sale
{
    public class SaleEntryRepository : RepositoryBase<SaleEntryEntity>, ISaleEntryRepository
    {
        public SaleEntryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override SaleEntryEntity Add(SaleEntryEntity saleEntryEntity)
        {
            var insertedEntity = Context.SaleEntry.Add(saleEntryEntity);
            Context.InvoiceInfo.Add(new InvoiceInfoEntity
            {
                InvoiceInfoId = Guid.NewGuid().ToString(),
                EntryId = insertedEntity.SaleEntryId,
                EntryType = Option.SALE_ENTRY,
                TenantId = saleEntryEntity.TenantId
            });
            Context.Commit();
            return insertedEntity;
        }

        public override SaleEntryEntity GetById(string saleEntryId)
        {
            return Context.SaleEntry.Include("Customer").Include("TenantInfo").FirstOrDefault(sale => sale.SaleEntryId == saleEntryId);
        }

        public override bool Delete(string saleEntryId)
        {
            var saleEntryEntity = Context.SaleEntry.Find(saleEntryId);

            if (saleEntryEntity != null)
            {
                var invoiceInfoEntity = Context.InvoiceInfo.FirstOrDefault(invinf => invinf.EntryId == saleEntryId);
                if (invoiceInfoEntity != null)
                {
                    Context.InvoiceInfo.Remove(invoiceInfoEntity);

                    var saleReturnEntities = Context.SaleReturn.Where(salrtn => salrtn.RefInvoiceId == invoiceInfoEntity.InvoiceInfoId);
                    foreach (SaleReturnEntity saleReturnEntity in saleReturnEntities)
                    {
                        invoiceInfoEntity = Context.InvoiceInfo.FirstOrDefault(invinf => invinf.EntryId == saleReturnEntity.SaleReturnId);
                        if (invoiceInfoEntity != null)
                        {
                            Context.InvoiceInfo.Remove(invoiceInfoEntity);
                        }
                        Context.Entry(saleReturnEntity).Collection("SaleReturnedProducts").Load();
                        var productReturnQuantities = saleReturnEntity.SaleReturnedProducts.ToList();
                        foreach (ProductReturnQuantityEntity productReturnQuantity in productReturnQuantities)
                        {
                            saleReturnEntity.SaleReturnedProducts.Remove(productReturnQuantity);
                            Context.ProductReturnQuantity.Remove(productReturnQuantity);
                        }
                        Context.SaleReturn.Remove(saleReturnEntity);
                    }
                }

                Context.Entry(saleEntryEntity).Collection("SaledProducts").Load();
                var productQuantities = saleEntryEntity.SaledProducts.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    saleEntryEntity.SaledProducts.Remove(productQuantity);
                    Context.ProductQuantity.Remove(productQuantity);
                }

                var tenantEntity = Context.Tenant.Find(saleEntryEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }

                Context.SaleEntry.Remove(saleEntryEntity);
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
