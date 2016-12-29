using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Model.Customer;
using Service.Tenant;
using System.Net.Http;
using System.Web.Http;
using Service.Customer;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("customerdue")]
    public class CustomerDueController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly ICustomerDueServices customerDueServices;

        public CustomerDueController(ICustomerDueServices customerDueServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
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

        [Route("deactivate/{id:length(36)}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateCustomerDue(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var customerDueEntity = customerDueServices.GetCustomerDue(id);
                if (customerDueEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(customerDueEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(customerDueEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "CustomerDue is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "CustomerDue has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "CustomerDue is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}