using System;
using AutoMapper;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Vat;
using Model.Accounts;
using ModernInfoTechInventory.ViewModels.Accounts;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

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
        public HttpResponseMessage PostInvestorTransaction(InvestorTransactionView investorTransactionView)
        {
            var investorTransactionEntity = new InvestorTransactionEntity
            {
                InvestorTransactionId = Guid.NewGuid().ToString(),
                TransactionDate = investorTransactionView.TransactionDate,
                Amount = investorTransactionView.Amount,
                Description = investorTransactionView.Description,
                TransactionType = investorTransactionView.TransactionType,
                InvestorId = investorTransactionView.InvestorId
            };
            var insertedEntity = investorTransactionServices.CreateInvestorTransaction(investorTransactionEntity);
            return GetInvestorTransaction(insertedEntity.InvestorTransactionId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutInvestorTransaction(string id, InvestorTransactionView investorTransactionView)
        {
            var investorTransactionEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<InvestorTransactionView, InvestorTransactionEntity>());
            var investorTransactionEntity = investorTransactionEntityMapper.CreateMapper().Map<InvestorTransactionView, InvestorTransactionEntity>(investorTransactionView);
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