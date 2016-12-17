using Data.Infrastructure;
using Data.Helper;
using Model.Sale;
using Model.InvoiceInfo;
using Model.Inventory;
using Model.Utilities;
using System.Linq;
using System;

namespace Data.Repositories.Sale
{
    public class SaleReturnRepository : RepositoryBase<SaleReturnEntity>, ISaleReturnRepository
    {
        public SaleReturnRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override SaleReturnEntity Add(SaleReturnEntity saleReturnEntity)
        {
            var insertedEntity = Context.SaleReturn.Add(saleReturnEntity);
            Context.InvoiceInfo.Add(
                new InvoiceInfoEntity
                {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    EntryId = insertedEntity.SaleReturnId,
                    EntryType = Option.SALE_RETURN,
                    Status = true
                });
            Context.Commit();
            return insertedEntity;
        }

        public override bool Delete(string saleReturnId)
        {
            var saleReturnEntity = Context.SaleReturn.Find(saleReturnId);

            if (saleReturnEntity != null)
            {
                var invoiceInfoEntity = Context.InvoiceInfo.FirstOrDefault(invinf => invinf.EntryId == saleReturnId);
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
