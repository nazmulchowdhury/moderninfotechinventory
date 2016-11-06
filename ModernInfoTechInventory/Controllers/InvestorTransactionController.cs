using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Investor;
using Model.Investor;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class InvestorTransactionController : ApiController
    {
        private readonly IInvestorTransactionServices investorTransactionServices;

        public InvestorTransactionController(IInvestorTransactionServices investorTransactionServices)
        {
            this.investorTransactionServices = investorTransactionServices;
        }

        public HttpResponseMessage GetAllInvestorTransactions()
        {
            var investorTransactionEntities = investorTransactionServices.GetAllInvestorTransactions().ToList();
            if (investorTransactionEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, investorTransactionEntities);
            }
            throw new ApiDataException(1000, "Investor Transactions are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetInvestorTransaction(string id)
        {
            var investorTransactionEntity = investorTransactionServices.GetInvestorTransaction(id);
            if (investorTransactionEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, investorTransactionEntity);
            }
            throw new ApiDataException(1001, "No Investor Transaction found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostInvestorTransaction(InvestorTransactionEntity investorTransactionEntity)
        {
            var insertedEntity = investorTransactionServices.CreateInvestorTransaction(investorTransactionEntity);
            return GetInvestorTransaction(insertedEntity.InvestorTransactionId);
        }

        public HttpResponseMessage PutInvestorTransaction(string id, InvestorTransactionEntity investorTransactionEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, investorTransactionServices.UpdateInvestorTransaction(id, investorTransactionEntity));
        }

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