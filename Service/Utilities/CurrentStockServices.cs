using Model.Utilities;
using System.Collections.Generic;
using Data.Repositories.Utilities;

namespace Service.Utilities
{
    public class CurrentStockServices : ICurrentStockServices
    {
        private readonly ICurrentStockRepository currentStockRepository;

        public CurrentStockServices(ICurrentStockRepository currentStockRepository)
        {
            this.currentStockRepository = currentStockRepository;
        }

        public CurrentStock GetCurrentStock(string productId)
        {
            var storedProduct = currentStockRepository.GetProduct(productId);

            if (storedProduct != null)
            {
                string productName = storedProduct.ProductName;
                int purchaseQuantity = 0;
                int purchaseReturnQuantity = 0;
                int saleQuantity = 0;
                int saleReturnQuantity = 0;
                int damageQuantity = 0;

                foreach (var product in currentStockRepository.GetStockedProductQuantities(productId, Option.PURCHASE_ENTRY))
                {
                    purchaseQuantity += product.Quantity;
                }

                foreach (var product in currentStockRepository.GetStockedProductReturnQuantities(productId, Option.PURCHASE_RETURN))
                {
                    purchaseReturnQuantity += product.ReturnQuantity;
                }

                foreach (var product in currentStockRepository.GetStockedProductQuantities(productId, Option.BILL_ENTRY))
                {
                    saleQuantity += product.Quantity;
                }

                foreach (var product in currentStockRepository.GetStockedProductReturnQuantities(productId, Option.SALE_RETURN))
                {
                    saleReturnQuantity += product.ReturnQuantity;
                }

                foreach (var product in currentStockRepository.GetStockedProductQuantities(productId, Option.DAMAGE_ENTRY))
                {
                    damageQuantity += product.Quantity;
                }

                return new CurrentStock
                {
                    ProductName = productName,
                    PurchaseQuantity = purchaseQuantity,
                    PurchaseReturnQuantity = purchaseReturnQuantity,
                    SaleQuantity = saleQuantity,
                    SaleReturnQuantity = saleReturnQuantity,
                    DamageQuantity = damageQuantity,
                    CurrentStockQuantity = purchaseQuantity + saleReturnQuantity - saleQuantity - purchaseReturnQuantity - damageQuantity
                };
            }
            else
            {
                return null;
            }
        }

        public ICollection<StockedProductQuantity> GetAllStockedProducts(string productId, Option option)
        {
            return currentStockRepository.GetStockedProductQuantities(productId, option);
        }

        public ICollection<StockedProductReturnQuantity> GetAllStockedProductReturns(string productId, Option option)
        {
            return currentStockRepository.GetStockedProductReturnQuantities(productId, option);
        }
    }
}
