using System.Collections.Generic;
using Model.Product;

namespace Service.Product
{
    public interface IProductInfoServices
    {
        IEnumerable<ProductInfoEntity> GetAllProducts();
        ProductInfoEntity GetProduct(string productId);
        ProductInfoEntity CreateProduct(ProductInfoEntity productInfoEntity);
        bool UpdateProduct(string productId, ProductInfoEntity productInfoEntity);
        bool DeleteProduct(string productId);
    }
}
