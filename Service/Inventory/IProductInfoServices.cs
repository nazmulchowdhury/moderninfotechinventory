using System.Collections.Generic;
using Model.Inventory;

namespace Service.Inventory
{
    public interface IProductInfoServices
    {
        ICollection<ProductInfoEntity> GetAllProducts();
        ICollection<ProductInfoEntity> GetAllProducts(string subCategoryId);
        ProductInfoEntity GetProduct(string productId);
        ProductInfoEntity CreateProduct(ProductInfoEntity productInfoEntity);
        bool UpdateProduct(string productId, ProductInfoEntity productInfoEntity);
        bool DeleteProduct(string productId);
    }
}
