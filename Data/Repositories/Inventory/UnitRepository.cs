using Data.Helper;
using System.Linq;
using Model.Inventory;
using Data.Infrastructure;

namespace Data.Repositories.Inventory
{
    public class UnitRepository : RepositoryBase<UnitEntity>, IUnitRepository
    {
        public UnitRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override UnitEntity GetById(string unitId)
        {
            return Context.Unit.Include("TenantInfo").FirstOrDefault(unit => unit.UnitId == unitId);
        }

        public override bool Delete(string unitId)
        {
            var unitEntity = Context.Unit.Find(unitId);
            if (unitEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(unitEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Unit.Remove(unitEntity);
                Context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
