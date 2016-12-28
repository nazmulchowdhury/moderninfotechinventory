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
    [RoutePrefix("cash")]
    public class CashController : ApiController
    {
        private readonly ICashServices cashServices;

        public CashController(ICashServices cashServices)
        {
            this.cashServices = cashServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllCashes()
        {
            var cashEntities = cashServices.GetAllCashes();
            if (cashEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, cashEntities);
            }
            throw new ApiDataException(1000, "Cashes are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetCash(string id)
        {
            var cashEntity = cashServices.GetCash(id);
            if (cashEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, cashEntity);
            }
            throw new ApiDataException(1001, "No Cash found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostCash(CashEntity cashEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            cashEntity.CashId = Guid.NewGuid().ToString();
            cashEntity.TenantId = tenantEntity.TenantId;
            cashEntity.TenantInfo = tenantEntity;
            var insertedEntity = cashServices.CreateCash(cashEntity);
            return GetCash(insertedEntity.CashId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteCash(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = cashServices.DeleteCash(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Cash is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}