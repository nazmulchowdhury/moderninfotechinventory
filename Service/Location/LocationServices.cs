﻿using Model.Location;
using System.Collections.Generic;
using Data.Repositories.Location;

namespace Service.Location
{
    public class LocationServices : ILocationServices
    {
        private readonly ILocationRepository locationRepository;

        public LocationServices(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public ICollection<LocationEntity> GetAllLocations()
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
            var storedItem = locationRepository.GetById(locationId);
            
            if (storedItem != null)
            {
                storedItem.LocationName = locationEntity.LocationName;
                storedItem.TenantInfo.UserId = locationEntity.TenantInfo.UserId;

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