using Model.Sale;
using Data.Repositories.Sale;
using System.Collections.Generic;

namespace Service.Sale
{
    public class SaleEntryServices : ISaleEntryServices
    {
        private readonly ISaleEntryRepository saleEntryRepository;

        public SaleEntryServices(ISaleEntryRepository saleEntryRepository)
        {
            this.saleEntryRepository = saleEntryRepository;
        }

        public ICollection<SaleEntryEntity> GetAllSaleEntries()
        {
            return saleEntryRepository.GetAll();
        }

        public SaleEntryEntity GetSaleEntry(string saleEntryId)
        {
            return saleEntryRepository.GetById(saleEntryId);
        }

        public SaleEntryEntity CreateSaleEntry(SaleEntryEntity saleEntryEntity)
        {
            return saleEntryRepository.Add(saleEntryEntity);
        }

        public bool UpdateSaleEntry(string saleEntryId, SaleEntryEntity saleEntryEntity)
        {
            var storedItem = saleEntryRepository.GetById(saleEntryId);

            if (storedItem != null)
            {
                storedItem.CustomerId = saleEntryEntity.CustomerId;
                storedItem.Discount = saleEntryEntity.Discount;
                storedItem.TenantInfo.UserId = saleEntryEntity.TenantInfo.UserId;

                saleEntryRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteSaleEntry(string saleEntryId)
        {
            return saleEntryRepository.Delete(saleEntryId);
        }
    }
}
