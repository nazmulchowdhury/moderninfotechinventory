using Data.Helper;
using System.Linq;
using Model.Location;
using Data.Infrastructure;

namespace Data.Repositories.Location
{
    public class LocationRepository : RepositoryBase<LocationEntity>, ILocationRepository
    {
        public LocationRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override LocationEntity GetById(string locationId)
        {
            return Context.Location.Include("TenantInfo").FirstOrDefault(loc => loc.LocationId == locationId);
        }

        public override bool Delete(string locationId)
        {
            var locationEntity = Context.Location.Find(locationId);
            if (locationEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(locationEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Location.Remove(locationEntity);
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
