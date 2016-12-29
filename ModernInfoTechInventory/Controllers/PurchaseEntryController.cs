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
    [RoutePrefix("purchaseentry")]
    public class PurchaseEntryController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IPurchaseEntryServices purchaseEntryServices;

        public PurchaseEntryController(IPurchaseEntryServices purchaseEntryServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.purchaseEntryServices = purchaseEntryServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllPurchaseEntries()
        {
            var purchaseEntryEntities = purchaseEntryServices.GetAllPurchaseEntries();
            if (purchaseEntryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryEntities);
            }
            throw new ApiDataException(1000, "Purchase Entries are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage GetPurchaseEntry(string id)
        {
            var purchaseEntryEntity = purchaseEntryServices.GetPurchaseEntry(id);
            if (purchaseEntryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryEntity);
            }
            throw new ApiDataException(1001, "No Purchase Entry found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostPurchaseEntry(PurchaseEntryEntity purchaseEntryEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            purchaseEntryEntity.PurchaseEntryId = Guid.NewGuid().ToString();
            purchaseEntryEntity.TenantId = tenantEntity.TenantId;
            purchaseEntryEntity.TenantInfo = tenantEntity;
            purchaseEntryEntity.PurchasedProducts = purchaseEntryEntity.PurchasedProducts.Select(purchasedProduct =>
            {
                purchasedProduct.ProductQuantityId = Guid.NewGuid().ToString();
                purchasedProduct.TenantId = tenantEntity.TenantId;
                return purchasedProduct;
            }).ToList();

            var insertedEntity = purchaseEntryServices.CreatePurchaseEntry(purchaseEntryEntity);
            return GetPurchaseEntry(insertedEntity.PurchaseEntryId);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutPurchaseEntry(string id, PurchaseEntryEntity purchaseEntryEntity)
        {
            purchaseEntryEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryServices.UpdatePurchaseEntry(id, purchaseEntryEntity));
        }

        [Route("{id:guid}")]
        public HttpResponseMessage DeletePurchaseEntry(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = purchaseEntryServices.DeletePurchaseEntry(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Purchase Entry is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivatePurchaseEntry(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var purchaseEntryEntity = purchaseEntryServices.GetPurchaseEntry(id);
                if (purchaseEntryEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(purchaseEntryEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(purchaseEntryEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Purchase Entry is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Purchase Entry has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Purchase Entry is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}