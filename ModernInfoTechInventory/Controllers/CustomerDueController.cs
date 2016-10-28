using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Customer;
using Model.Customer;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class CustomerDueController : ApiController
    {
        private readonly ICustomerDueServices customerDueServices;

        public CustomerDueController(ICustomerDueServices customerDueServices)
        {
            this.customerDueServices = customerDueServices;
        }

        public HttpResponseMessage GetAllCustomerDues()
        {
            var customerDueEntities = customerDueServices.GetAllCustomerDues().ToList();
            if (customerDueEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerDueEntities);
            }
            throw new ApiDataException(1000, "CustomerDues are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetCustomerDue(string id)
        {
            var customerDueEntity = customerDueServices.GetCustomerDue(id);
            if (customerDueEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerDueEntity);
            }
            throw new ApiDataException(1001, "No CustomerDue found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostCustomerDue(CustomerDueEntity customerDueEntity)
        {
            var insertedEntity = customerDueServices.CreateCustomerDue(customerDueEntity);
            return GetCustomerDue(insertedEntity.CustomerDueId);
        }

        public HttpResponseMessage PutCustomerDue(string id, CustomerDueEntity customerDueEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, customerDueServices.UpdateCustomerDue(id, customerDueEntity));
        }

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