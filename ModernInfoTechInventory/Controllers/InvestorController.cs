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
    [RoutePrefix("investor")]
    public class InvestorController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IInvestorServices investorServices;

        public InvestorController(IInvestorServices investorServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.investorServices = investorServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllInvestors()
        {
            var investorEntities = investorServices.GetAllInvestors();
            if (investorEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, investorEntities);
            }
            throw new ApiDataException(1000, "Investors are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage GetInvestor(string id)
        {
            var investorEntity = investorServices.GetInvestor(id);
            if (investorEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, investorEntity);
            }
            throw new ApiDataException(1001, "No Investor found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostInvestor(InvestorEntity investorEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            investorEntity.InvestorId = Guid.NewGuid().ToString();
            investorEntity.TenantId = tenantEntity.TenantId;
            investorEntity.TenantInfo = tenantEntity;
            var insertedEntity = investorServices.CreateInvestor(investorEntity);
            return GetInvestor(insertedEntity.InvestorId);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutInvestor(string id, InvestorEntity investorEntity)
        {
            investorEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, investorServices.UpdateInvestor(id, investorEntity));
        }

        [Route("{id:guid}")]
        public HttpResponseMessage DeleteInvestor(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = investorServices.DeleteInvestor(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Investor is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateInvestor(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var investorEntity = investorServices.GetInvestor(id);
                if (investorEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(investorEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(investorEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Investor is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Investor has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Investor has already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}