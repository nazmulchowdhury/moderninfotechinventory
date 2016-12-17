using Data.Infrastructure;
using Data.Helper;
using Model.Inventory;

namespace Data.Repositories.Inventory
{
    public class UnitRepository : RepositoryBase<UnitEntity>, IUnitRepository
    {
        public UnitRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
