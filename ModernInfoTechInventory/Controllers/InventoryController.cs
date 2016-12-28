using System;
using System.Net;
using System.Linq;
using Model.Inventory;
using Model.BaseModel;
using System.Net.Http;
using System.Web.Http;
using Service.Inventory;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("inventory")]
    public class InventoryController : ApiController
    {
        private readonly IInventoryServices inventoryServices;

        public InventoryController(IInventoryServices inventoryServices)
        {
            this.inventoryServices = inventoryServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllInventories()
        {
            var inventoryEntities = inventoryServices.GetAllInventories();
            if (inventoryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, inventoryEntities);
            }
            throw new ApiDataException(1000, "Inventories are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetInventory(string id)
        {
            var inventoryEntity = inventoryServices.GetInventory(id);
            if (inventoryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, inventoryEntity);
            }
            throw new ApiDataException(1001, "No Inventory found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostInventory(InventoryEntity inventoryEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            inventoryEntity.InventoryId = Guid.NewGuid().ToString();
            inventoryEntity.TenantId = inventoryEntity.TenantId;
            inventoryEntity.TenantInfo = tenantEntity;
            var insertedEntity = inventoryServices.CreateInventory(inventoryEntity);
            return GetInventory(insertedEntity.InventoryId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutInventory(string id, InventoryEntity inventoryEntity)
        {
            inventoryEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, inventoryServices.UpdateInventory(id, inventoryEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteInventory(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = inventoryServices.DeleteInventory(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Inventory is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}