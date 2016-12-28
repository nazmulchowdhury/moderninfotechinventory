using System;
using System.Net;
using System.Linq;
using Model.Customer;
using Model.BaseModel;
using System.Net.Http;
using System.Web.Http;
using Service.Customer;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;

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
        public HttpResponseMessage PostCustomerDue(CustomerDueEntity customerDueEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            customerDueEntity.CustomerDueId = Guid.NewGuid().ToString();
            customerDueEntity.TenantId = tenantEntity.TenantId;
            customerDueEntity.TenantInfo = tenantEntity;
            var insertedEntity = customerDueServices.CreateCustomerDue(customerDueEntity);
            return GetCustomerDue(insertedEntity.CustomerDueId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutCustomerDue(string id, CustomerDueEntity customerDueEntity)
        {
            customerDueEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
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