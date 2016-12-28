using System;
using System.Net;
using System.Linq;
using Model.Purchase;
using Model.Inventory;
using Model.BaseModel;
using System.Net.Http;
using System.Web.Http;
using Service.Purchase;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("purchaseentry")]
    public class PurchaseEntryController : ApiController
    {
        private readonly IPurchaseEntryServices purchaseEntryServices;

        public PurchaseEntryController(IPurchaseEntryServices purchaseEntryServices)
        {
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

        [Route("{id:length(36)}")]
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

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutPurchaseEntry(string id, PurchaseEntryEntity purchaseEntryEntity)
        {
            purchaseEntryEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryServices.UpdatePurchaseEntry(id, purchaseEntryEntity));
        }

        [Route("{id:length(36)}")]
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
    }
}