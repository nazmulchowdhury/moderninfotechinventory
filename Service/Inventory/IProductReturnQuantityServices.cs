using System.Collections.Generic;
using Model.Inventory;

namespace Service.Inventory
{
    public interface IProductReturnQuantityServices
    {
        ICollection<ProductReturnQuantityEntity> GetAllProductReturnQuantities();
        ProductReturnQuantityEntity GetProductReturnQuantity(string productReturnQuantityId);
        ProductReturnQuantityEntity CreateProductReturnQuantity(ProductReturnQuantityEntity productReturnQuantityEntity);
        bool UpdateProductReturnQuantity(string productReturnQuantityId, ProductReturnQuantityEntity productReturnQuantityEntity);
        bool DeleteProductReturnQuantity(string productReturnQuantityId);
    }
}
