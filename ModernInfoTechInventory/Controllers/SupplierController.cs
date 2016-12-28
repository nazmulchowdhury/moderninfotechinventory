using System;
using System.Net;
using System.Linq;
using Model.Supplier;
using Model.BaseModel;
using System.Net.Http;
using System.Web.Http;
using Service.Supplier;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("supplier")]
    public class SupplierController : ApiController
    {
        private readonly ISupplierServices supplierServices;

        public SupplierController(ISupplierServices supplierServices)
        {
            this.supplierServices = supplierServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllSuppliers()
        {
            var supplierEntities = supplierServices.GetAllSuppliers();
            if (supplierEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierEntities);
            }
            throw new ApiDataException(1000, "Suppliers are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetSupplier(string id)
        {
            var supplierEntity = supplierServices.GetSupplier(id);
            if (supplierEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierEntity);
            }
            throw new ApiDataException(1001, "No Supplier found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostSupplier(SupplierEntity supplierEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            supplierEntity.SupplierId = Guid.NewGuid().ToString();
            supplierEntity.TenantId = tenantEntity.TenantId;
            supplierEntity.TenantInfo = tenantEntity;
            var insertedEntity = supplierServices.CreateSupplier(supplierEntity);
            return GetSupplier(insertedEntity.SupplierId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutSupplier(string id, SupplierEntity supplierEntity)
        {
            supplierEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, supplierServices.UpdateSupplier(id, supplierEntity));
        }

        [Route("{id:length(36)}")]
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