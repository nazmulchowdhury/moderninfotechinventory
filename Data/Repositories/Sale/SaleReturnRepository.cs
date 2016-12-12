using Data.Infrastructure;
using Data.Helper;
using Model.Sale;
using Model.InvoiceInfo;
using Model.Inventory;
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
            var insertedEntity = DbContext.SaleReturn.Add(saleReturnEntity);
            DbContext.InvoiceInfo.Add(
                new InvoiceInfoEntity
                {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    SaleReturnId = insertedEntity.SaleReturnId,
                    Status = true
                });
            DbContext.Commit();
            return insertedEntity;
        }

        public override bool Delete(string saleReturnId)
        {
            var saleReturnEntity = DbContext.SaleReturn.Find(saleReturnId);

            if (saleReturnEntity != null)
            {
                var invoiceInfoEntity = DbContext.InvoiceInfo.FirstOrDefault(invinf => invinf.SaleReturnId == saleReturnId);
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
