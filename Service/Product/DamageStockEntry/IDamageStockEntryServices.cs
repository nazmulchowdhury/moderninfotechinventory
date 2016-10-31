﻿using System.Collections.Generic;
using Model.Product;

namespace Service.Product.DamageStockEntry
{
    public interface IDamageStockEntryServices
    {
        IEnumerable<DamageStockEntryEntity> GetAllDamageStockEntries();
        DamageStockEntryEntity GetDamageStockEntry(string damageStockEntryId);
        DamageStockEntryEntity CreateDamageStockEntry(DamageStockEntryEntity damageStockEntryEntity);
        bool UpdateDamageStockEntry(string damageStockEntryId, DamageStockEntryEntity damageStockEntryEntity);
        bool DeleteDamageStockEntry(string damageStockEntryId);
    }
}
