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
    [RoutePrefix("investment")]
    public class InvestmentController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly InvestmentServices investmentServices;

        public InvestmentController(InvestmentServices investmentServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.investmentServices = investmentServices;
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetInvestmentAmount(string id)
        {
            var investmentEntity = investmentServices.InvestmentAmount(id);
            if (investmentEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, investmentEntity.Amount);
            }
            throw new ApiDataException(1001, "No Investment found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostInvestment(InvestmentEntity investmentEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            investmentEntity.InvestmentId = Guid.NewGuid().ToString();
            investmentEntity.TenantId = tenantEntity.TenantId;
            investmentEntity.TenantInfo = tenantEntity;
            var insertedEntity = investmentServices.CreateInvestment(investmentEntity);
            return Request.CreateResponse(HttpStatusCode.OK, investmentServices.InvestmentAmount(insertedEntity.InvestmentId));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteInvestment(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = investmentServices.DeleteInvestment(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Failed to delete.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:length(36)}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateInvestment(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var investmentEntity = investmentServices.InvestmentAmount(id);
                if (investmentEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(investmentEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(investmentEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Investment is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Investment has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Investment has already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}