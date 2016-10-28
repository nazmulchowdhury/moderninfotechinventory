using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Supplier;
using Model.Supplier;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class SupplierPaymentController : ApiController
    {
        private readonly ISupplierPaymentServices supplierPaymentServices;

        public SupplierPaymentController(ISupplierPaymentServices supplierPaymentServices)
        {
            this.supplierPaymentServices = supplierPaymentServices;
        }

        public HttpResponseMessage GetAllSupplierPayments()
        {
            var supplierPaymentEntities = supplierPaymentServices.GetAllSupplierPayments().ToList();
            if (supplierPaymentEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentEntities);
            }
            throw new ApiDataException(1000, "SupplierPayments are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetSupplierPayment(string id)
        {
            var supplierPaymentEntity = supplierPaymentServices.GetSupplierPayment(id);
            if (supplierPaymentEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentEntity);
            }
            throw new ApiDataException(1001, "No SupplierPayment found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostSupplierPayment(SupplierPaymentEntity supplierPaymentEntity)
        {
            var insertedEntity = supplierPaymentServices.CreateSupplierPayment(supplierPaymentEntity);
            return GetSupplierPayment(insertedEntity.SupplierPaymentId);
        }

        public HttpResponseMessage PutSupplierPayment(string id, SupplierPaymentEntity supplierPaymentEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentServices.UpdateSupplierPayment(id, supplierPaymentEntity));
        }

        public HttpResponseMessage DeleteSupplierPayment(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = supplierPaymentServices.DeleteSupplierPayment(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "SupplierPayment is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}