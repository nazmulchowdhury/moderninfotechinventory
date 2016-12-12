using Data.Infrastructure;
using Data.Helper;
using Model.Sale;
using Model.Inventory;
using Model.InvoiceInfo;
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
            var insertedEntity = DbContext.BillEntry.Add(billEntryEntity);
            DbContext.InvoiceInfo.Add(
                new InvoiceInfoEntity
                {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    BillEntryId = insertedEntity.BillEntryId,
                    Status = true
                });
            DbContext.Commit();
            return insertedEntity;
        }

        public override BillEntryEntity GetById(string billEntryId)
        {
            return DbContext.BillEntry.Include("Customer").FirstOrDefault(bill => bill.BillEntryId == billEntryId);
        }

        public override bool Delete(string billEntryId)
        {
            var billEntryEntity = DbContext.BillEntry.Find(billEntryId);

            if (billEntryEntity != null)
            {
                var invoiceInfoEntity = DbContext.InvoiceInfo.FirstOrDefault(invinf => invinf.BillEntryId == billEntryId);
                if (invoiceInfoEntity != null)
                {
                    DbContext.InvoiceInfo.Remove(invoiceInfoEntity);

                    var saleReturnEntities = DbContext.SaleReturn.Where(salrtn => salrtn.RefInvoiceId == invoiceInfoEntity.InvoiceInfoId);
                    foreach (SaleReturnEntity saleReturnEntity in saleReturnEntities)
                    {
                        invoiceInfoEntity = DbContext.InvoiceInfo.FirstOrDefault(invinf => invinf.SaleReturnId == saleReturnEntity.SaleReturnId);
                        if (invoiceInfoEntity != null)
                        {
                            DbContext.InvoiceInfo.Remove(invoiceInfoEntity);
                        }
                        DbContext.Entry(saleReturnEntity).Collection("ProductReturnQuantities").Load();
                        var productReturnQuantities = saleReturnEntity.ProductReturnQuantities.ToList();
                        foreach (ProductReturnQuantityEntity productReturnQuantity in productReturnQuantities)
                        {
                            saleReturnEntity.ProductReturnQuantities.Remove(productReturnQuantity);
                            DbContext.ProductReturnQuantity.Remove(productReturnQuantity);
                        }
                        DbContext.SaleReturn.Remove(saleReturnEntity);
                    }
                }

                DbContext.Entry(billEntryEntity).Collection("ProductQuantities").Load();
                var productQuantities = billEntryEntity.ProductQuantities.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    billEntryEntity.ProductQuantities.Remove(productQuantity);
                    DbContext.ProductQuantity.Remove(productQuantity);
                }

                DbContext.BillEntry.Remove(billEntryEntity);
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
