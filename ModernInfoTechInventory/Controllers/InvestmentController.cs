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
    [RoutePrefix("investment")]
    public class InvestmentController : ApiController
    {
        private readonly InvestmentServices investmentServices;

        public InvestmentController(InvestmentServices investmentServices)
        {
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
    }
}