using Model.Location;
using System.Collections.Generic;

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
