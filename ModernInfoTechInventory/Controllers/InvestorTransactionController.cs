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
    [RoutePrefix("investortransaction")]
    public class InvestorTransactionController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IInvestorTransactionServices investorTransactionServices;

        public InvestorTransactionController(IInvestorTransactionServices investorTransactionServices,
            ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
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

        [Route("deactivate/{id:length(36)}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateInvestorTransaction(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var investorTransactionEntity = investorTransactionServices.GetInvestorTransaction(id);
                if (investorTransactionEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(investorTransactionEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(investorTransactionEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "InvestorTransaction is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "InvestorTransaction has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "InvestorTransaction has already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}