﻿using System;
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
    [RoutePrefix("customer")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerServices customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            this.customerServices = customerServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllCustomers()
        {
            var customerEntities = customerServices.GetAllCustomers();
            if (customerEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerEntities);
            }
            throw new ApiDataException(1000, "Customers are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetCustomer(string id)
        {
            var customerEntity = customerServices.GetCustomer(id);
            if (customerEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerEntity);
            }
            throw new ApiDataException(1001, "No Customer found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostCustomer(CustomerEntity customerEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            customerEntity.CustomerId = Guid.NewGuid().ToString();
            customerEntity.TenantId = tenantEntity.TenantId;
            customerEntity.TenantInfo = tenantEntity;
            var insertedEntity = customerServices.CreateCustomer(customerEntity);
            return GetCustomer(insertedEntity.CustomerId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutCustomer(string id, CustomerEntity customerEntity)
        {
            customerEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, customerServices.UpdateCustomer(id, customerEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteCustomer(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = customerServices.DeleteCustomer(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Customer is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}