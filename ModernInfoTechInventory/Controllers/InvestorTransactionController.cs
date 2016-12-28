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
    [RoutePrefix("investortransaction")]
    public class InvestorTransactionController : ApiController
    {
        private readonly IInvestorTransactionServices investorTransactionServices;

        public InvestorTransactionController(IInvestorTransactionServices investorTransactionServices)
        {
            this.investorTransactionServices = investorTransactionServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllInvestorTransactions()
        {
            var investorTransactionEntities = investorTransactionServices.GetAllInvestorTransactions();
            if (investorTransactionEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, investorTransactionEntities);
            }
            throw new ApiDataException(1000, "Investor Transactions are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetInvestorTransaction(string id)
        {
            var investorTransactionEntity = investorTransactionServices.GetInvestorTransaction(id);
            if (investorTransactionEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, investorTransactionEntity);
            }
            throw new ApiDataException(1001, "No Investor Transaction found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostInvestorTransaction(InvestorTransactionEntity investorTransactionEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            investorTransactionEntity.InvestorTransactionId = Guid.NewGuid().ToString();
            investorTransactionEntity.TenantId = tenantEntity.TenantId;
            investorTransactionEntity.TenantInfo = tenantEntity;
            var insertedEntity = investorTransactionServices.CreateInvestorTransaction(investorTransactionEntity);
            return GetInvestorTransaction(insertedEntity.InvestorTransactionId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutInvestorTransaction(string id, InvestorTransactionEntity investorTransactionEntity)
        {
            investorTransactionEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, investorTransactionServices.UpdateInvestorTransaction(id, investorTransactionEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteInvestorTransaction(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = investorTransactionServices.DeleteInvestorTransaction(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Investor Transaction is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}