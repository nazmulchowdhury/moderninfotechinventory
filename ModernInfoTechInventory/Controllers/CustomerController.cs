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
    public class CustomerController : ApiController
    {
        private readonly ICustomerServices customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            this.customerServices = customerServices;
        }

        public HttpResponseMessage GetAllCustomers()
        {
            var customerEntities = customerServices.GetAllCustomers().ToList();
            if (customerEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerEntities);
            }
            throw new ApiDataException(1000, "Customers are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetCustomer(string id)
        {
            var customerEntity = customerServices.GetCustomer(id);
            if (customerEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerEntity);
            }
            throw new ApiDataException(1001, "No Customer found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostCustomer(CustomerEntity customerEntity)
        {
            var insertedEntity = customerServices.CreateCustomer(customerEntity);
            return GetCustomer(insertedEntity.CustomerId);
        }

        public HttpResponseMessage PutCustomer(string id, CustomerEntity customerEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, customerServices.UpdateCustomer(id, customerEntity));
        }

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