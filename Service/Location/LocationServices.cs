using System.Collections.Generic;
using Data.Repositories.Location;
using Model.Location;

namespace Service.Location
{
    public class LocationServices : ILocationServices
    {
        private readonly ILocationRepository locationRepository;

        public LocationServices(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public IEnumerable<LocationEntity> GetAllLocations()
        {
            return locationRepository.GetAll();
        }

        public LocationEntity GetLocation(string locationId)
        {
            return locationRepository.GetById(locationId);
        }
        
        public LocationEntity CreateLocation(LocationEntity locationEntity)
        {
            return locationRepository.Add(locationEntity);
        }
        
        public bool UpdateLocation(string locationId, LocationEntity locationEntity)
        {
            LocationEntity storedItem = locationRepository.GetById(locationId);
            
            if (storedItem != null)
            {
                storedItem.LocationName = locationEntity.LocationName;
                locationRepository.Update(storedItem);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteLocation(string locationId)
        {
            return locationRepository.Delete(locationId);
        }
    }
}