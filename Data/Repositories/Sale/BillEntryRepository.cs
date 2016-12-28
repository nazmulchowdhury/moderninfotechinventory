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
    public class BillEntryRepository : RepositoryBase<BillEntryEntity>, IBillEntryRepository
    {
        public BillEntryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override BillEntryEntity Add(BillEntryEntity billEntryEntity)
        {
            var insertedEntity = Context.BillEntry.Add(billEntryEntity);
            Context.InvoiceInfo.Add(new InvoiceInfoEntity
            {
                InvoiceInfoId = Guid.NewGuid().ToString(),
                EntryId = insertedEntity.BillEntryId,
                EntryType = Option.BILL_ENTRY,
                TenantId = billEntryEntity.TenantId
            });
            Context.Commit();
            return insertedEntity;
        }

        public override BillEntryEntity GetById(string billEntryId)
        {
            return Context.BillEntry.Include("Customer").Include("TenantInfo").FirstOrDefault(bill => bill.BillEntryId == billEntryId);
        }

        public override bool Delete(string billEntryId)
        {
            var billEntryEntity = Context.BillEntry.Find(billEntryId);

            if (billEntryEntity != null)
            {
                var invoiceInfoEntity = Context.InvoiceInfo.FirstOrDefault(invinf => invinf.EntryId == billEntryId);
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

                Context.Entry(billEntryEntity).Collection("SaledProducts").Load();
                var productQuantities = billEntryEntity.SaledProducts.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    billEntryEntity.SaledProducts.Remove(productQuantity);
                    Context.ProductQuantity.Remove(productQuantity);
                }

                var tenantEntity = Context.Tenant.Find(billEntryEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }

                Context.BillEntry.Remove(billEntryEntity);
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
