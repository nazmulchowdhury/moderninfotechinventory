using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Investment;
using Model.Investment;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class InvestmentController : ApiController
    {
        private readonly InvestmentServices investmentServices;

        public InvestmentController(InvestmentServices investmentServices)
        {
            this.investmentServices = investmentServices;
        }

        public HttpResponseMessage GetInvestmentAmount(string id)
        {
            var investmentEntity = investmentServices.InvestmentAmount(id);
            if (investmentEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, investmentEntity.Amount);
            }
            throw new ApiDataException(1001, "No Investment found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostInvestment(InvestmentEntity investmentEntity)
        {
            var insertedEntity = investmentServices.CreateInvestment(investmentEntity);
            return Request.CreateResponse(HttpStatusCode.OK, investmentServices.InvestmentAmount(insertedEntity.InvestmentId));
        }

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