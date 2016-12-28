using Model.Sale;
using Data.Repositories.Sale;
using System.Collections.Generic;

namespace Service.Sale
{
    public class BillEntryServices : IBillEntryServices
    {
        private readonly IBillEntryRepository billEntryRepository;

        public BillEntryServices(IBillEntryRepository billEntryRepository)
        {
            this.billEntryRepository = billEntryRepository;
        }

        public ICollection<BillEntryEntity> GetAllBillEntries()
        {
            return billEntryRepository.GetAll();
        }

        public BillEntryEntity GetBillEntry(string billEntryId)
        {
            return billEntryRepository.GetById(billEntryId);
        }

        public BillEntryEntity CreateBillEntry(BillEntryEntity billEntryEntity)
        {
            return billEntryRepository.Add(billEntryEntity);
        }

        public bool UpdateBillEntry(string billEntryId, BillEntryEntity billEntryEntity)
        {
            var storedItem = billEntryRepository.GetById(billEntryId);

            if (storedItem != null)
            {
                storedItem.CustomerId = billEntryEntity.CustomerId;
                storedItem.Discount = billEntryEntity.Discount;
                storedItem.TenantInfo.UserId = billEntryEntity.TenantInfo.UserId;

                billEntryRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteBillEntry(string billEntryId)
        {
            return billEntryRepository.Delete(billEntryId);
        }
    }
}
