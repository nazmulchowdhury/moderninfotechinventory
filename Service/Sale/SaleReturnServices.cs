using Model.Sale;
using Data.Repositories.Sale;
using System.Collections.Generic;

namespace Service.Sale
{
    public class SaleReturnServices : ISaleReturnServices
    {
        private readonly ISaleReturnRepository saleReturnRepository;

        public SaleReturnServices(ISaleReturnRepository saleReturnRepository)
        {
            this.saleReturnRepository = saleReturnRepository;
        }

        public ICollection<SaleReturnEntity> GetAllSaleReturns()
        {
            return saleReturnRepository.GetAll();
        }

        public SaleReturnEntity GetSaleReturn(string saleReturnId)
        {
            return saleReturnRepository.GetById(saleReturnId);
        }

        public SaleReturnEntity CreateSaleReturn(SaleReturnEntity saleReturnEntity)
        {
            return saleReturnRepository.Add(saleReturnEntity);
        }

        public bool UpdateSaleReturn(string saleReturnId, SaleReturnEntity saleReturnEntity)
        {
            var storedItem = saleReturnRepository.GetById(saleReturnId);

            if (storedItem != null)
            {
                storedItem.RefInvoiceId = saleReturnEntity.RefInvoiceId;
                storedItem.Penalty = saleReturnEntity.Penalty;
                storedItem.PaidAmount = saleReturnEntity.PaidAmount;
                storedItem.TenantInfo.UserId = saleReturnEntity.TenantInfo.UserId;

                saleReturnRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteSaleReturn(string saleReturnId)
        {
            return saleReturnRepository.Delete(saleReturnId);
        }
    }
}