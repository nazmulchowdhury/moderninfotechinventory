using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using Model.Accounts;
using System.Net.Http;
using System.Web.Http;
using Service.Accounts;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("vat")]
    public class VatController : ApiController
    {
        private readonly IVatServices vatServices;
        private readonly ITenantServices tenantServices;

        public VatController(IVatServices vatServices, ITenantServices tenantServices)
        {
            this.vatServices = vatServices;
            this.tenantServices = tenantServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllVats()
        {
            var vatEntities = vatServices.GetAllVats();
            if (vatEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, vatEntities);
            }
            throw new ApiDataException(1000, "Vats are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetVat(string id)
        {
            var vatEntity = vatServices.GetVat(id);
            if (vatEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, vatEntity);
            }
            throw new ApiDataException(1001, "No Vat found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostVat(VatEntity vatEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            vatEntity.VatId = Guid.NewGuid().ToString();
            vatEntity.TenantId = tenantEntity.TenantId;
            vatEntity.TenantInfo = tenantEntity;
            var insertedEntity = vatServices.CreateVat(vatEntity);
            return GetVat(insertedEntity.VatId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutVat(string id, VatEntity vatEntity)
        {
            vatEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, vatServices.UpdateVat(id, vatEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteVat(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = vatServices.DeleteVat(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Vat is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:length(36)}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateVat(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var vatEntity = vatServices.GetVat(id);
                if (vatEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(vatEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(vatEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Vat is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Vat has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Vat has already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}