using Model.Inventory;
using System.Collections.Generic;

namespace Service.Inventory
{
    public interface IUnitServices
    {
        ICollection<UnitEntity> GetAllUnitEntities();
        UnitEntity GetUnit(string unitId);
        UnitEntity CreateUnit(UnitEntity unitEntity);
        bool UpdateUnit(string unitId, UnitEntity unitEntity);
        bool DeleteUnit(string unitId);
    }
}
