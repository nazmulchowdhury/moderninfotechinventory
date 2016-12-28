using Model.Inventory;
using System.Collections.Generic;

namespace Service.Inventory
{
    public interface IProductQuantityServices
    {
        ICollection<ProductQuantityEntity> GetAllProductQuantities();
        ProductQuantityEntity GetProductQuantity(string productQuantityId);
        ProductQuantityEntity CreateProductQuantity(ProductQuantityEntity productQuantityEntity);
        bool UpdateProductQuantity(string productQuantityId, ProductQuantityEntity productQuantityEntity);
        bool DeleteProductQuantity(string productQuantityId);
    }
}
