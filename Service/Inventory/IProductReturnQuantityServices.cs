using Model.Inventory;
using System.Collections.Generic;

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
