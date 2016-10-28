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
    public class SupplierController : ApiController
    {
        private readonly ISupplierServices supplierServices;

        public SupplierController(ISupplierServices supplierServices)
        {
            this.supplierServices = supplierServices;
        }

        public HttpResponseMessage GetAllSuppliers()
        {
            var supplierEntities = supplierServices.GetAllSuppliers().ToList();
            if (supplierEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierEntities);
            }
            throw new ApiDataException(1000, "Suppliers are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetSupplier(string id)
        {
            var supplierEntity = supplierServices.GetSupplier(id);
            if (supplierEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierEntity);
            }
            throw new ApiDataException(1001, "No Supplier found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostSupplier(SupplierEntity supplierEntity)
        {
            var insertedEntity = supplierServices.CreateSupplier(supplierEntity);
            return GetSupplier(insertedEntity.SupplierId);
        }

        public HttpResponseMessage PutSupplier(string id, SupplierEntity supplierEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, supplierServices.UpdateSupplier(id, supplierEntity));
        }

        public HttpResponseMessage DeleteSupplier(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = supplierServices.DeleteSupplier(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Supplier is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}