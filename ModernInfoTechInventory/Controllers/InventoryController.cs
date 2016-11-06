using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Inventory;
using Model.Inventory;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        private readonly IInventoryServices inventoryServices;

        public InventoryController(IInventoryServices inventoryServices)
        {
            this.inventoryServices = inventoryServices;
        }

        public HttpResponseMessage GetAllInventories()
        {
            var inventoryEntities = inventoryServices.GetAllInventories().ToList();
            if (inventoryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, inventoryEntities);
            }
            throw new ApiDataException(1000, "Inventories are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetInventory(string id)
        {
            var inventoryEntity = inventoryServices.GetInventory(id);
            if (inventoryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, inventoryEntity);
            }
            throw new ApiDataException(1001, "No Inventory found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostInventory(InventoryEntity inventoryEntity)
        {
            var insertedEntity = inventoryServices.CreateInventory(inventoryEntity);
            return GetInventory(insertedEntity.InventoryId);
        }

        public HttpResponseMessage PutInventory(string id, InventoryEntity inventoryEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, inventoryServices.UpdateInventory(id, inventoryEntity));
        }

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