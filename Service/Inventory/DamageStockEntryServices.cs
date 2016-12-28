using Model.Inventory;
using System.Collections.Generic;
using Data.Repositories.Inventory;

namespace Service.Inventory
{
    public class DamageStockEntryServices : IDamageStockEntryServices
    {
        private readonly IDamageStockEntryRepository damageStockEntryRepository;

        public DamageStockEntryServices(IDamageStockEntryRepository damageStockEntryRepository)
        {
            this.damageStockEntryRepository = damageStockEntryRepository;
        }

        public ICollection<DamageStockEntryEntity> GetAllDamageStockEntries()
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
                storedItem.TenantInfo.UserId = damageStockEntryEntity.TenantInfo.UserId;

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
