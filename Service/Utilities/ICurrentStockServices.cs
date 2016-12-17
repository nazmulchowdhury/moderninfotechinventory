using Model.Utilities;
using System.Collections.Generic;

namespace Service.Utilities
{
    public interface ICurrentStockServices
    {
        CurrentStock GetCurrentStock(string productId);
        ICollection<StockedProductQuantity> GetAllStockedProducts(string productId, Option option);
        ICollection<StockedProductReturnQuantity> GetAllStockedProductReturns(string productId, Option option);
    }
}
