using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using Model.Inventory;
using System.Net.Http;
using System.Web.Http;
using Service.Inventory;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("unit")]
    public class UnitController : ApiController
    {
        private readonly IUnitServices unitServices;
        private readonly ITenantServices tenantServices;

        public UnitController(IUnitServices unitServices, ITenantServices tenantServices)
        {
            this.unitServices = unitServices;
            this.tenantServices = tenantServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllUnits()
        {
            var unitEntities = unitServices.GetAllUnitEntities();
            if (unitEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, unitEntities);
            }
            throw new ApiDataException(1000, "Units are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage GetUnit(string id)
        {
            var unitEntity = unitServices.GetUnit(id);
            if (unitEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, unitEntity);
            }
            throw new ApiDataException(1001, "No Unit found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostUnit(UnitEntity unitEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            unitEntity.UnitId = Guid.NewGuid().ToString();
            unitEntity.TenantId = tenantEntity.TenantId;
            unitEntity.TenantInfo = tenantEntity;
            var insertedEntity = unitServices.CreateUnit(unitEntity);
            return GetUnit(insertedEntity.UnitId);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutUnit(string id, UnitEntity unitEntity)
        {
            unitEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, unitServices.UpdateUnit(id, unitEntity));
        }

        [Route("{id:guid}")]
        public HttpResponseMessage DeleteUnit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = unitServices.DeleteUnit(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Unit is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateUnit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var unitEntity = unitServices.GetUnit(id);
                if (unitEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(unitEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(unitEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Unit is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Unit has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Unit is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}