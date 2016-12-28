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
    [RoutePrefix("supplierpayment")]
    public class SupplierPaymentController : ApiController
    {
        private readonly ISupplierPaymentServices supplierPaymentServices;

        public SupplierPaymentController(ISupplierPaymentServices supplierPaymentServices)
        {
            this.supplierPaymentServices = supplierPaymentServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllSupplierPayments()
        {
            var supplierPaymentEntities = supplierPaymentServices.GetAllSupplierPayments();
            if (supplierPaymentEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentEntities);
            }
            throw new ApiDataException(1000, "SupplierPayments are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetSupplierPayment(string id)
        {
            var supplierPaymentEntity = supplierPaymentServices.GetSupplierPayment(id);
            if (supplierPaymentEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentEntity);
            }
            throw new ApiDataException(1001, "No SupplierPayment found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostSupplierPayment(SupplierPaymentEntity supplierPaymentEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            supplierPaymentEntity.SupplierPaymentId = Guid.NewGuid().ToString();
            supplierPaymentEntity.TenantId = tenantEntity.TenantId;
            supplierPaymentEntity.TenantInfo = tenantEntity;
            var insertedEntity = supplierPaymentServices.CreateSupplierPayment(supplierPaymentEntity);
            return GetSupplierPayment(insertedEntity.SupplierPaymentId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutSupplierPayment(string id, SupplierPaymentEntity supplierPaymentEntity)
        {
            supplierPaymentEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentServices.UpdateSupplierPayment(id, supplierPaymentEntity));
        }

        [Route("{id:length(36)}")]
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