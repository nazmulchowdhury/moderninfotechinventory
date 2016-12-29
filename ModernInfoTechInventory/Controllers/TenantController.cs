using System;
using System.Net;
using System.Linq;
using Service.Tenant;
using Model.Tenant;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("tenant")]
    public class TenantController : ApiController
    {
        private readonly ITenantServices tenantServices;

        public TenantController(ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllTenants()
        {
            var tenantEntities = tenantServices.GetAllTenants();
            if (tenantEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, tenantEntities);
            }
            throw new ApiDataException(1000, "Tenants are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage GetTenant(string id)
        {
            var tenantEntity = tenantServices.GetTenant(id);
            if (tenantEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, tenantEntity);
            }
            throw new ApiDataException(1001, "No Tenant found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutTenant(string id, TenantEntity tenantEntity)
        {
            tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
            tenantEntity.ActivationDate = DateTime.Now;
            tenantEntity.InactivationDate = DateTime.Now;
            return Request.CreateResponse(HttpStatusCode.OK, tenantServices.UpdateTenant(id, tenantEntity));
        }

        [Route("{id:guid}")]
        public HttpResponseMessage DeleteTenant(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = tenantServices.DeleteTenant(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Tenant is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}