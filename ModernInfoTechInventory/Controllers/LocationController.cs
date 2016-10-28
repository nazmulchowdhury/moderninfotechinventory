using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Location;
using Model.Location;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class LocationController : ApiController
    {
        private readonly LocationServices locationServices;

        public LocationController(LocationServices locationServices)
        {
            this.locationServices = locationServices;
        }

        public HttpResponseMessage GetAllLocations()
        {
            var locationEntities = locationServices.GetAllLocations().ToList();
            if (locationEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, locationEntities);
            }
            throw new ApiDataException(1000, "Locations are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetLocation(string id)
        {
            var locationEntity = locationServices.GetLocation(id);
            if (locationEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, locationEntity);
            }
            throw new ApiDataException(1001, "No Location found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostLocation(LocationEntity locationEntity)
        {
            var insertedEntity = locationServices.CreateLocation(locationEntity);
            return GetLocation(insertedEntity.LocationId);
        }

        public HttpResponseMessage PutLocation(string id, LocationEntity locationEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, locationServices.UpdateLocation(id, locationEntity));
        }

        public HttpResponseMessage DeleteLocation(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = locationServices.DeleteLocation(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Location is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}