using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using Model.Location;
using System.Net.Http;
using System.Web.Http;
using Service.Location;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("location")]
    public class LocationController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly LocationServices locationServices;

        public LocationController(LocationServices locationServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.locationServices = locationServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllLocations()
        {
            var locationEntities = locationServices.GetAllLocations();
            if (locationEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, locationEntities);
            }
            throw new ApiDataException(1000, "Locations are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage GetLocation(string id)
        {
            var locationEntity = locationServices.GetLocation(id);
            if (locationEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, locationEntity);
            }
            throw new ApiDataException(1001, "No Location found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostLocation(LocationEntity locationEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            locationEntity.TenantId = tenantEntity.TenantId;
            locationEntity.TenantInfo = tenantEntity;
            locationEntity.LocationId = Guid.NewGuid().ToString();
            var insertedEntity = locationServices.CreateLocation(locationEntity);
            return GetLocation(insertedEntity.LocationId);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutLocation(string id, LocationEntity locationEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, locationServices.UpdateLocation(id, locationEntity));
        }

        [Route("{id:guid}")]
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

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateLocation(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var locationEntity = locationServices.GetLocation(id);
                if (locationEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(locationEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(locationEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Location is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Location has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Location is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}