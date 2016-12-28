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
    public class SaleReturnRepository : RepositoryBase<SaleReturnEntity>, ISaleReturnRepository
    {
        public SaleReturnRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override SaleReturnEntity GetById(string saleReturnId)
        {
            return Context.SaleReturn.Include("TenantInfo").FirstOrDefault(salrtn => salrtn.SaleReturnId == saleReturnId);
        }

        public override SaleReturnEntity Add(SaleReturnEntity saleReturnEntity)
        {
            var insertedEntity = Context.SaleReturn.Add(saleReturnEntity);
            Context.InvoiceInfo.Add(new InvoiceInfoEntity
            {
                InvoiceInfoId = Guid.NewGuid().ToString(),
                EntryId = insertedEntity.SaleReturnId,
                EntryType = Option.SALE_RETURN,
                TenantId = saleReturnEntity.TenantId
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

                Context.Entry(saleReturnEntity).Collection("SaleReturnedProducts").Load();
                var productReturnQuantities = saleReturnEntity.SaleReturnedProducts.ToList();
                foreach (ProductReturnQuantityEntity productReturnQuantity in productReturnQuantities)
                {
                    saleReturnEntity.SaleReturnedProducts.Remove(productReturnQuantity);
                    Context.ProductReturnQuantity.Remove(productReturnQuantity);
                }

                var tenantEntity = Context.Tenant.Find(saleReturnEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
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
