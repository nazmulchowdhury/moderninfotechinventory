using Data.Infrastructure;
using Data.Helper;
using Model.Sale;
using Model.Inventory;
using Model.InvoiceInfo;
using Model.Utilities;
using System;
using System.Linq;

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
            Context.InvoiceInfo.Add(
                new InvoiceInfoEntity
                {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    EntryId = insertedEntity.BillEntryId,
                    EntryType = Option.BILL_ENTRY,
                    Status = true
                });
            Context.Commit();
            return insertedEntity;
        }

        public override BillEntryEntity GetById(string billEntryId)
        {
            return Context.BillEntry.Include("Customer").FirstOrDefault(bill => bill.BillEntryId == billEntryId);
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
                        invoiceInfoEntity = Context.InvoiceInfo.FirstOrDefault(invinf => invinf.EntryId== saleReturnEntity.SaleReturnId);
                        if (invoiceInfoEntity != null)
                        {
                            Context.InvoiceInfo.Remove(invoiceInfoEntity);
                        }
                        Context.Entry(saleReturnEntity).Collection("ProductReturnQuantities").Load();
                        var productReturnQuantities = saleReturnEntity.ProductReturnQuantities.ToList();
                        foreach (ProductReturnQuantityEntity productReturnQuantity in productReturnQuantities)
                        {
                            saleReturnEntity.ProductReturnQuantities.Remove(productReturnQuantity);
                            Context.ProductReturnQuantity.Remove(productReturnQuantity);
                        }
                        Context.SaleReturn.Remove(saleReturnEntity);
                    }
                }

                Context.Entry(billEntryEntity).Collection("ProductQuantities").Load();
                var productQuantities = billEntryEntity.ProductQuantities.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    billEntryEntity.ProductQuantities.Remove(productQuantity);
                    Context.ProductQuantity.Remove(productQuantity);
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
