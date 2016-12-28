using Model.Inventory;
using System.Collections.Generic;

namespace Service.Inventory
{
    public interface IDamageStockEntryServices
    {
        ICollection<DamageStockEntryEntity> GetAllDamageStockEntries();
        DamageStockEntryEntity GetDamageStockEntry(string damageStockEntryId);
        DamageStockEntryEntity CreateDamageStockEntry(DamageStockEntryEntity damageStockEntryEntity);
        bool UpdateDamageStockEntry(string damageStockEntryId, DamageStockEntryEntity damageStockEntryEntity);
        bool DeleteDamageStockEntry(string damageStockEntryId);
    }
}
