using System;
using System.Net;
using System.Linq;
using Model.Accounts;
using Model.BaseModel;
using System.Net.Http;
using System.Web.Http;
using Service.Accounts;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("vat")]
    public class VatController : ApiController
    {
        private readonly IVatServices vatServices;

        public VatController(IVatServices vatServices)
        {
            this.vatServices = vatServices;
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
    }
}