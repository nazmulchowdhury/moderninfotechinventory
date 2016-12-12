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
        public HttpResponseMessage PostInvestor(InvestorView investorView)
        {
            var investorEntity = new InvestorEntity
            {
                InvestorId = Guid.NewGuid().ToString(),
                InvestorName = investorView.InvestorName,
                LocationId = investorView.LocationId,
                PhoneNumber = investorView.PhoneNumber,
                Balance = investorView.Balance
            };
            var insertedEntity = investorServices.CreateInvestor(investorEntity);
            return GetInvestor(insertedEntity.InvestorId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutInvestor(string id, InvestorView investorView)
        {
            var investorEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<InvestorView, InvestorEntity>());
            var investorEntity = investorEntityMapper.CreateMapper().Map<InvestorView, InvestorEntity>(investorView);
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