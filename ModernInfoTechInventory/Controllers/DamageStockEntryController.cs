using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Model.Inventory;
using Model.BaseModel;
using Service.Inventory;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("damagestockentry")]
    public class DamageStockEntryController : ApiController
    {
        private readonly IDamageStockEntryServices damageStockEntryServices;
        private readonly IProductQuantityServices productQuantityServices;

        public DamageStockEntryController(IDamageStockEntryServices damageStockEntryServices, IProductQuantityServices productQuantityServices)
        {
            this.damageStockEntryServices = damageStockEntryServices;
            this.productQuantityServices = productQuantityServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllDamageStockEntries()
        {
            var damageStockEntryEntities = damageStockEntryServices.GetAllDamageStockEntries();
            if (damageStockEntryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, damageStockEntryEntities);
            }
            throw new ApiDataException(1000, "Damage Stock Entries are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetDamageStockEntry(string id)
        {
            var damageStockEntryEntity = damageStockEntryServices.GetDamageStockEntry(id);
            if (damageStockEntryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, damageStockEntryEntity);
            }
            throw new ApiDataException(1001, "No Damage Stock Entry found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostDamageStockEntry(DamageStockEntryView damageStockEntryView)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            var productQuantityEntity = new ProductQuantityEntity
            {
                ProductQuantityId = Guid.NewGuid().ToString(),
                ProductId = damageStockEntryView.ProductId,
                Quantity = damageStockEntryView.Quantity,
                TenantId = tenantEntity.TenantId
            };

            var damageStockEntryEntity = new DamageStockEntryEntity
            {
                DamageStockEntryId = Guid.NewGuid().ToString(),
                Remark = damageStockEntryView.Remark,
                ProductQuantity = productQuantityEntity,
                ProductQuantityId = productQuantityEntity.ProductQuantityId,
                TenantId = tenantEntity.TenantId,
                TenantInfo = tenantEntity
            };
            var insertedEntity = damageStockEntryServices.CreateDamageStockEntry(damageStockEntryEntity);
            return GetDamageStockEntry(insertedEntity.DamageStockEntryId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutdamageStockEntry(string id, DamageStockEntryView damageStockEntryView)
        {
            var damageStockEntryEntity = damageStockEntryServices.GetDamageStockEntry(id);
            var productQuantityEntity = productQuantityServices.GetProductQuantity(damageStockEntryEntity.ProductQuantityId);
            productQuantityEntity.ProductId = damageStockEntryView.ProductId;
            productQuantityEntity.Quantity = damageStockEntryView.Quantity;
            damageStockEntryEntity.Remark = damageStockEntryView.Remark;
            damageStockEntryEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };

            productQuantityServices.UpdateProductQuantity(damageStockEntryEntity.ProductQuantityId, productQuantityEntity);
            return Request.CreateResponse(HttpStatusCode.OK, damageStockEntryServices.UpdateDamageStockEntry(id, damageStockEntryEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeletedamageStockEntry(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = damageStockEntryServices.DeleteDamageStockEntry(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Damage Stock Entry is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}