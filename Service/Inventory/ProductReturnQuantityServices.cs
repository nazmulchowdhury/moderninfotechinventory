using Model.Inventory;
using System.Collections.Generic;
using Data.Repositories.Inventory;

namespace Service.Inventory
{
    public class ProductReturnQuantityServices : IProductReturnQuantityServices
    {
        private readonly IProductReturnQuantityRepository productReturnQuantityRepository;

        public ProductReturnQuantityServices(IProductReturnQuantityRepository productReturnQuantityRepository)
        {
            this.productReturnQuantityRepository = productReturnQuantityRepository;
        }

        public ICollection<ProductReturnQuantityEntity> GetAllProductReturnQuantities()
        {
            return productReturnQuantityRepository.GetAll();
        }

        public ProductReturnQuantityEntity GetProductReturnQuantity(string productReturnQuantityId)
        {
            return productReturnQuantityRepository.GetById(productReturnQuantityId);
        }

        public ProductReturnQuantityEntity CreateProductReturnQuantity(ProductReturnQuantityEntity productReturnQuantityEntity)
        {
            return productReturnQuantityRepository.Add(productReturnQuantityEntity);
        }

        public bool UpdateProductReturnQuantity(string productReturnQuantityId, ProductReturnQuantityEntity productReturnQuantityEntity)
        {
            var storedItem = productReturnQuantityRepository.GetById(productReturnQuantityId);

            if (storedItem != null)
            {
                storedItem.ProductQuantityId = productReturnQuantityEntity.ProductQuantityId;
                storedItem.ReturnQuantity = productReturnQuantityEntity.ReturnQuantity;
                storedItem.TenantInfo.UserId = productReturnQuantityEntity.TenantInfo.UserId;

                productReturnQuantityRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProductReturnQuantity(string productReturnQuantityId)
        {
            return productReturnQuantityRepository.Delete(productReturnQuantityId);
        }
    }
}
