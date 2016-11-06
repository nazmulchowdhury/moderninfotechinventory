using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Purchase;
using Model.Purchase;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class PurchaseReturnController : ApiController
    {
        private readonly IPurchaseReturnServices purchaseReturnServices;

        public PurchaseReturnController(IPurchaseReturnServices purchaseReturnServices)
        {
            this.purchaseReturnServices = purchaseReturnServices;
        }

        public HttpResponseMessage GetAllPurchaseReturns()
        {
            var purchaseReturnsEntities = purchaseReturnServices.GetAllPurchaseReturns().ToList();
            if (purchaseReturnsEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseReturnsEntities);
            }
            throw new ApiDataException(1000, "Purchase Returns are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetPurchaseReturn(string id)
        {
            var purchaseReturnEntity = purchaseReturnServices.GetPurchaseReturn(id);
            if (purchaseReturnEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseReturnEntity);
            }
            throw new ApiDataException(1001, "No Purchase Return found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostPurchaseReturn(PurchaseReturnEntity purchaseReturnEntity)
        {
            var insertedEntity = purchaseReturnServices.CreatePurchaseReturn(purchaseReturnEntity);
            return GetPurchaseReturn(insertedEntity.PurchaseReturnId);
        }

        public HttpResponseMessage PutPurchaseReturn(string id, PurchaseReturnEntity purchaseReturnEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, purchaseReturnServices.UpdatePurchaseReturn(id, purchaseReturnEntity));
        }

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
    }
}