using System.Collections.Generic;
using Data.Repositories.Product.ProductInfo;
using Model.Product;

namespace Service.Product.ProductInfo
{
    public class ProductInfoServices : IProductInfoServices
    {
        private readonly IProductInfoRepository productInfoRepository;

        public ProductInfoServices(IProductInfoRepository productInfoRepository)
        {
            this.productInfoRepository = productInfoRepository;
        }

        public IEnumerable<ProductInfoEntity> GetAllProducts()
        {
            return productInfoRepository.GetAll();
        }

        public ProductInfoEntity GetProduct(string productId)
        {
            return productInfoRepository.GetById(productId);
        }

        public ProductInfoEntity CreateProduct(ProductInfoEntity productInfoEntity)
        {
            return productInfoRepository.Add(productInfoEntity);
        }

        public bool UpdateProduct(string productId, ProductInfoEntity productInfoEntity)
        {
            var storedItem = productInfoRepository.GetById(productId);

            if (storedItem != null)
            {
                storedItem.ProductName = productInfoEntity.ProductName;
                storedItem.Barcode = productInfoEntity.Barcode;
                storedItem.CostPrice = productInfoEntity.CostPrice;
                storedItem.SalePrice = productInfoEntity.SalePrice;
                storedItem.ReorderLevel = productInfoEntity.ReorderLevel;
                storedItem.SubCategoryId = productInfoEntity.SubCategoryId;

                productInfoRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProduct(string productId)
        {
            return productInfoRepository.Delete(productId);
        }
    }
}
