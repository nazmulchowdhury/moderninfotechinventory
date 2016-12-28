using Model.Inventory;
using System.Collections.Generic;
using Data.Repositories.Inventory;

namespace Service.Inventory
{
    public class UnitServices : IUnitServices
    {
        private readonly IUnitRepository unitRepository;

        public UnitServices(IUnitRepository unitRepository)
        {
            this.unitRepository = unitRepository;
        }

        public ICollection<UnitEntity> GetAllUnitEntities()
        {
            return unitRepository.GetAll();
        }

        public UnitEntity GetUnit(string unitId)
        {
            return unitRepository.GetById(unitId);
        }

        public UnitEntity CreateUnit(UnitEntity unitEntity)
        {
            return unitRepository.Add(unitEntity);
        }

        public bool UpdateUnit(string unitId, UnitEntity unitEntity)
        {
            var storedItem = unitRepository.GetById(unitId);

            if (storedItem != null)
            {
                storedItem.UnitName = unitEntity.UnitName;
                storedItem.TenantInfo.UserId = unitEntity.TenantInfo.UserId;

                unitRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUnit(string unitId)
        {
            return unitRepository.Delete(unitId);
        }
    }
}
