using System.Collections.Generic;
using Data.Repositories.Product.ProductInfo;
using Model.Product;

namespace Service.Product.ProductInfo
{
    public class ProductQuantityServices : IProductQuantityServices
    {
        private readonly IProductQuantityRepository productQuantityRepository;

        public ProductQuantityServices(IProductQuantityRepository productQuantityRepository)
        {
            this.productQuantityRepository = productQuantityRepository;
        }

        public IEnumerable<ProductQuantityEntity> GetAllProductQuantities()
        {
            return productQuantityRepository.GetAll();
        }

        public ProductQuantityEntity GetProductQuantity(string productQuantityId)
        {
            return productQuantityRepository.GetById(productQuantityId);
        }

        public ProductQuantityEntity CreateProductQuantity(ProductQuantityEntity productQuantityEntity)
        {
            return productQuantityRepository.Add(productQuantityEntity);
        }

        public bool UpdateProductQuantity(string productQuantityId, ProductQuantityEntity productQuantityEntity)
        {
            var storedItem = productQuantityRepository.GetById(productQuantityId);

            if (storedItem != null)
            {
                storedItem.Quantity = productQuantityEntity.Quantity;
                storedItem.CustomerId = productQuantityEntity.CustomerId;

                productQuantityRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProductQuantity(string productQuantityId)
        {
            return productQuantityRepository.Delete(productQuantityId);
        }
    }
}
