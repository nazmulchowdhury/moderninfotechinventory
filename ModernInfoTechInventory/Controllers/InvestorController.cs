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
    [RoutePrefix("investor")]
    public class InvestorController : ApiController
    {
        private readonly IInvestorServices investorServices;

        public InvestorController(IInvestorServices investorServices)
        {
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

        [Route("{id:length(36)}")]
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

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutInvestor(string id, InvestorEntity investorEntity)
        {
            investorEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, investorServices.UpdateInvestor(id, investorEntity));
        }

        [Route("{id:length(36)}")]
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
    }
}