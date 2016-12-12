using System.Collections.Generic;
using Model.Location;

namespace Service.Location
{
    public interface ILocationServices
    {
        ICollection<LocationEntity> GetAllLocations();
        LocationEntity GetLocation(string locationId);
        LocationEntity CreateLocation(LocationEntity locationEntity);
        bool UpdateLocation(string locationId, LocationEntity locationEntity);
        bool DeleteLocation(string locationId);
    }
}
