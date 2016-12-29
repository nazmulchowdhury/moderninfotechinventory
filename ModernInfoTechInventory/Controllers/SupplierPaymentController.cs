using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using Model.Supplier;
using System.Net.Http;
using System.Web.Http;
using Service.Supplier;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("supplierpayment")]
    public class SupplierPaymentController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly ISupplierPaymentServices supplierPaymentServices;

        public SupplierPaymentController(ISupplierPaymentServices supplierPaymentServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
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
            throw new ApiDataException(1000, "Supplier Payments are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage GetSupplierPayment(string id)
        {
            var supplierPaymentEntity = supplierPaymentServices.GetSupplierPayment(id);
            if (supplierPaymentEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentEntity);
            }
            throw new ApiDataException(1001, "No Supplier Payment found for this " + id, HttpStatusCode.NotFound);
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

        [Route("{id:guid}")]
        public HttpResponseMessage PutSupplierPayment(string id, SupplierPaymentEntity supplierPaymentEntity)
        {
            supplierPaymentEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentServices.UpdateSupplierPayment(id, supplierPaymentEntity));
        }

        [Route("{id:guid}")]
        public HttpResponseMessage DeleteSupplierPayment(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = supplierPaymentServices.DeleteSupplierPayment(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Supplier Payment is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateSupplierPayment(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var supplierPaymentEntity = supplierPaymentServices.GetSupplierPayment(id);
                if (supplierPaymentEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(supplierPaymentEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(supplierPaymentEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Supplier Payment is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Supplier Payment has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Supplier Payment is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}