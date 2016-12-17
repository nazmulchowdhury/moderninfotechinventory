using Model.Utilities;
using Model.Inventory;
using System.Collections.Generic;

namespace Data.Repositories.Utilities
{
    public interface ICurrentStockRepository
    {
        ProductInfoEntity GetProduct(string productId);
        ICollection<StockedProductQuantity> GetStockedProductQuantities(string productId, Option option);
        ICollection<StockedProductReturnQuantity> GetStockedProductReturnQuantities(string productId, Option option);
    }
}
