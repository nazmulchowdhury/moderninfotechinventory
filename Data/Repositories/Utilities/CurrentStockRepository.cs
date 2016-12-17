using System.Linq;
using Data.Helper;
using Model.Utilities;
using Model.Inventory;
using Data.Infrastructure;
using System.Collections.Generic;

namespace Data.Repositories.Utilities
{
    public class CurrentStockRepository : ICurrentStockRepository
    {
        private readonly ModernInfoTechInventoryContext context;
        private readonly IDbFactory factory;

        public CurrentStockRepository(IDbFactory factory)
        {
            this.factory = factory;
            context = factory.Init();
        }

        public ProductInfoEntity GetProduct(string productId)
        {
            return context.ProductInfo.Find(productId);
        }

        public ICollection<StockedProductQuantity> GetStockedProductQuantities(string productId, Option option)
        {
            string billingQuery = @"select ProductQuantityId, p.ProductId, ProductName, Quantity from dbo.Product p
                                    inner join (select pq.ProductQuantityId, ProductId, Quantity from dbo.ProductQuantity pq
                                    inner join BillEntryProductQuantity bpq on pq.ProductQuantityId = bpq.ProductQuantityId
                                    where ProductId = @p0) pj on p.ProductId = pj.ProductId";

            string purchasingQuery = @"select ProductQuantityId, p.ProductId, ProductName, Quantity from dbo.Product p
                                       inner join (select pq.ProductQuantityId, ProductId, Quantity from dbo.ProductQuantity pq
                                       inner join PurchaseEntryProductQuantity pepq on pq.ProductQuantityId = pepq.ProductQuantityId
                                       where ProductId = @p0) pj on p.ProductId = pj.ProductId";

            string damageStockReturningQuery = @"select ProductQuantityId, p.ProductId, ProductName, Quantity from dbo.Product p
                                                 inner join (select pq.ProductQuantityId, ProductId, Quantity from dbo.ProductQuantity pq
                                                 inner join DamageStockEntry dse on pq.ProductQuantityId = dse.ProductQuantityId
                                                 where ProductId = @p0) pj on p.ProductId = pj.ProductId";

            switch (option)
            {
                case (Option.BILL_ENTRY): return context.StockedProductQuantity.SqlQuery(billingQuery, productId).ToList();
                case (Option.PURCHASE_ENTRY): return context.StockedProductQuantity.SqlQuery(purchasingQuery, productId).ToList();
                case (Option.DAMAGE_ENTRY): return context.StockedProductQuantity.SqlQuery(damageStockReturningQuery, productId).ToList();
                default: return new List<StockedProductQuantity>();
            }
        }

        public ICollection<StockedProductReturnQuantity> GetStockedProductReturnQuantities(string productId, Option option)
        {
            string saleReturningQuery = @"select pq.ProductQuantityId, p.ProductId, p.ProductName, prq.ReturnQuantity
                                          from SaleReturnQuantity srq 
                                          inner join ProductReturnQuantity prq on srq.ProductReturnQuantityId = prq.ProductReturnQuantityId
                                          inner join BillEntryProductQuantity bepq on prq.ProductQuantityId = bepq.ProductQuantityId
                                          inner join ProductQuantity pq on prq.ProductQuantityId = pq.ProductQuantityId
                                          inner join Product p on pq.ProductId = p.ProductId
                                          where p.ProductId = @p0";

            string purchaseReturningQuery = @"select pq.ProductQuantityId, p.ProductId, p.ProductName, prq.ReturnQuantity
                                          from PurchaseReturnQuantity perq 
                                          inner join ProductReturnQuantity prq on perq.ProductReturnQuantityId = prq.ProductReturnQuantityId
                                          inner join PurchaseEntryProductQuantity pepq on prq.ProductQuantityId = pepq.ProductQuantityId
                                          inner join ProductQuantity pq on prq.ProductQuantityId = pq.ProductQuantityId
                                          inner join Product p on pq.ProductId = p.ProductId
                                          where p.ProductId = @p0";

            switch (option)
            {
                case (Option.SALE_RETURN): return context.StockedProductReturnQuantity.SqlQuery(saleReturningQuery, productId).ToList();
                case (Option.PURCHASE_RETURN): return context.StockedProductReturnQuantity.SqlQuery(purchaseReturningQuery, productId).ToList();
                default: return new List<StockedProductReturnQuantity>();
            }
        }
    }
}
