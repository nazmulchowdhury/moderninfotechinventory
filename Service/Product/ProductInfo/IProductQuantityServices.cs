using System.Collections.Generic;
using Model.Product;

namespace Service.Product.ProductInfo
{
    public interface IProductQuantityServices
    {
        IEnumerable<ProductQuantityEntity> GetAllProductQuantities();
        ProductQuantityEntity GetProductQuantity(string productQuantityId);
        ProductQuantityEntity CreateProductQuantity(ProductQuantityEntity productQuantityEntity);
        bool UpdateProductQuantity(string productQuantityId, ProductQuantityEntity productQuantityEntity);
        bool DeleteProductQuantity(string productQuantityId);
    }
}
