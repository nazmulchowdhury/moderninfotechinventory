using System.Collections.Generic;
using Data.Repositories.Product;
using Model.Product;

namespace Service.Product
{
    public class DamageStockEntryServices : IDamageStockEntryServices
    {
        private readonly IDamageStockEntryRepository damageStockEntryRepository;

        public DamageStockEntryServices(IDamageStockEntryRepository damageStockEntryRepository)
        {
            this.damageStockEntryRepository = damageStockEntryRepository;
        }

        public IEnumerable<DamageStockEntryEntity> GetAllDamageStockEntries()
        {
            return damageStockEntryRepository.GetAll();
        }

        public DamageStockEntryEntity GetDamageStockEntry(string damageStockEntryId)
        {
            return damageStockEntryRepository.GetById(damageStockEntryId);
        }

        public DamageStockEntryEntity CreateDamageStockEntry(DamageStockEntryEntity damageStockEntryEntity)
        {
            return damageStockEntryRepository.Add(damageStockEntryEntity);
        }

        public bool UpdateDamageStockEntry(string damageStockEntryId, DamageStockEntryEntity damageStockEntryEntity)
        {
            var storedItem = damageStockEntryRepository.GetById(damageStockEntryId);

            if (storedItem != null)
            {
                storedItem.ProductQuantityId = damageStockEntryEntity.ProductQuantityId;
                storedItem.Remark = damageStockEntryEntity.Remark;

                damageStockEntryRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDamageStockEntry(string damageStockEntryId)
        {
            return damageStockEntryRepository.Delete(damageStockEntryId);
        }
    }
}
