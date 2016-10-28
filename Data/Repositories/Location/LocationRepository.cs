using Data.Infrastructure;
using Data.Helper;
using Model.Location;

namespace Data.Repositories.Location
{
    public class LocationRepository : RepositoryBase<LocationEntity>, ILocationRepository
    {
        public LocationRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
