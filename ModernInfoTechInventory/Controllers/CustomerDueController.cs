using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Service.Customer;
using Model.Customer;
using ModernInfoTechInventory.ViewModels.Customer;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("customerdue")]
    public class CustomerDueController : ApiController
    {
        private readonly ICustomerDueServices customerDueServices;

        public CustomerDueController(ICustomerDueServices customerDueServices)
        {
            this.customerDueServices = customerDueServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllCustomerDues()
        {
            var customerDueEntities = customerDueServices.GetAllCustomerDues();
            if (customerDueEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerDueEntities);
            }
            throw new ApiDataException(1000, "CustomerDues are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetCustomerDue(string id)
        {
            var customerDueEntity = customerDueServices.GetCustomerDue(id);
            if (customerDueEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerDueEntity);
            }
            throw new ApiDataException(1001, "No CustomerDue found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostCustomerDue(CustomerDueView customerDueView)
        {
            var customerDueEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDueView, CustomerDueEntity>()
                    .ConstructUsing((CustomerDueView cdv) =>
                    {
                        var cde = new CustomerDueEntity();
                        cde.CustomerDueId = Guid.NewGuid().ToString();
                        return cde;
                    }));

            var customerDueEntity = customerDueEntityMapper.CreateMapper().Map<CustomerDueView, CustomerDueEntity>(customerDueView);

            var insertedEntity = customerDueServices.CreateCustomerDue(customerDueEntity);
            return GetCustomerDue(insertedEntity.CustomerDueId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutCustomerDue(string id, CustomerDueView customerDueView)
        {
            var customerDueEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDueView, CustomerDueEntity>());
            var customerDueEntity = customerDueEntityMapper.CreateMapper().Map<CustomerDueView, CustomerDueEntity>(customerDueView);

            return Request.CreateResponse(HttpStatusCode.OK, customerDueServices.UpdateCustomerDue(id, customerDueEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteCustomerDue(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = customerDueServices.DeleteCustomerDue(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "CustomerDue is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}