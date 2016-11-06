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
    public class InvestorController : ApiController
    {
        private readonly IInvestorServices investorServices;

        public InvestorController(IInvestorServices investorServices)
        {
            this.investorServices = investorServices;
        }

        public HttpResponseMessage GetAllInvestors()
        {
            var investorEntities = investorServices.GetAllInvestors().ToList();
            if (investorEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, investorEntities);
            }
            throw new ApiDataException(1000, "Investors are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetInvestor(string id)
        {
            var investorEntity = investorServices.GetInvestor(id);
            if (investorEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, investorEntity);
            }
            throw new ApiDataException(1001, "No Investor found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostInvestor(InvestorEntity investorEntity)
        {
            var insertedEntity = investorServices.CreateInvestor(investorEntity);
            return GetInvestor(insertedEntity.InvestorId);
        }

        public HttpResponseMessage PutInvestor(string id, InvestorEntity investorEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, investorServices.UpdateInvestor(id, investorEntity));
        }

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