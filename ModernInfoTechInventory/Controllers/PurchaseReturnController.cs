using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Model.Purchase;
using Service.Tenant;
using Model.Inventory;
using System.Net.Http;
using System.Web.Http;
using Service.Purchase;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("purchasereturn")]
    public class PurchaseReturnController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IPurchaseReturnServices purchaseReturnServices;

        public PurchaseReturnController(IPurchaseReturnServices purchaseReturnServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.purchaseReturnServices = purchaseReturnServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllPurchaseReturns()
        {
            var purchaseReturnsEntities = purchaseReturnServices.GetAllPurchaseReturns();
            if (purchaseReturnsEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseReturnsEntities);
            }
            throw new ApiDataException(1000, "Purchase Returns are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetPurchaseReturn(string id)
        {
            var purchaseReturnEntity = purchaseReturnServices.GetPurchaseReturn(id);
            if (purchaseReturnEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseReturnEntity);
            }
            throw new ApiDataException(1001, "No Purchase Return found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostPurchaseReturn(PurchaseReturnEntity purchaseReturnEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            purchaseReturnEntity.PurchaseReturnId = Guid.NewGuid().ToString();
            purchaseReturnEntity.TenantId = tenantEntity.TenantId;
            purchaseReturnEntity.TenantInfo = tenantEntity;
            purchaseReturnEntity.PurchaseReturnedProducts = purchaseReturnEntity.PurchaseReturnedProducts.Select(productReturnQuantity =>
            {
                productReturnQuantity.ProductReturnQuantityId = Guid.NewGuid().ToString();
                purchaseReturnEntity.TenantId = tenantEntity.TenantId;
                return productReturnQuantity;
            }).ToList();

            var insertedEntity = purchaseReturnServices.CreatePurchaseReturn(purchaseReturnEntity);
            return GetPurchaseReturn(insertedEntity.PurchaseReturnId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutPurchaseReturn(string id, PurchaseReturnEntity purchaseReturnEntity)
        {
            purchaseReturnEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, purchaseReturnServices.UpdatePurchaseReturn(id, purchaseReturnEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeletePurchaseReturn(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = purchaseReturnServices.DeletePurchaseReturn(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Purchase Return is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:length(36)}")]
        [HttpDelete]
        public HttpResponseMessage DeactivatePurchaseReturn(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var purchaseReturnEntity = purchaseReturnServices.GetPurchaseReturn(id);
                if (purchaseReturnEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(purchaseReturnEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(purchaseReturnEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Purchase Return is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Purchase Return has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Purchase Return is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}