using System.Collections.Generic;
using Data.Repositories.Sale.SaleReturn;
using Model.Sale;

namespace Service.Sale.SaleReturn
{
    public class SaleReturnQuantityServices : ISaleReturnQuantityServices
    {
        private readonly SaleReturnQuantityRepository saleReturnQuantityRepository;

        public SaleReturnQuantityServices(SaleReturnQuantityRepository saleReturnQuantityRepository)
        {
            this.saleReturnQuantityRepository = saleReturnQuantityRepository;
        }

        public IEnumerable<SaleReturnQuantityEntity> GetAllSaleReturnQuantities()
        {
            return saleReturnQuantityRepository.GetAll();
        }

        public SaleReturnQuantityEntity GetSaleReturnQuantity(string saleReturnQuantityId)
        {
            return saleReturnQuantityRepository.GetById(saleReturnQuantityId);
        }

        public SaleReturnQuantityEntity CreateSaleReturnQuantity(SaleReturnQuantityEntity saleReturnQuantityEntity)
        {
            return saleReturnQuantityRepository.Add(saleReturnQuantityEntity);
        }

        public bool UpdateSaleReturnQuantity(string saleReturnQuantityId, SaleReturnQuantityEntity saleReturnQuantityEntity)
        {
            var storedItem = saleReturnQuantityRepository.GetById(saleReturnQuantityId);

            if (storedItem != null)
            {
                storedItem.ReturnQuantity = saleReturnQuantityEntity.ReturnQuantity;

                saleReturnQuantityRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteSaleReturnQuantity(string saleReturnQuantityId)
        {
            return saleReturnQuantityRepository.Delete(saleReturnQuantityId);
        }
    }
}
