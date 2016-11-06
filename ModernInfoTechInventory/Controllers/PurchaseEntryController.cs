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
    public class PurchaseEntryController : ApiController
    {
        private readonly IPurchaseEntryServices purchaseEntryServices;

        public PurchaseEntryController(IPurchaseEntryServices purchaseEntryServices)
        {
            this.purchaseEntryServices = purchaseEntryServices;
        }

        public HttpResponseMessage GetAllPurchaseEntries()
        {
            var purchaseEntryEntities = purchaseEntryServices.GetAllPurchaseEntries().ToList();
            if (purchaseEntryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryEntities);
            }
            throw new ApiDataException(1000, "Purchase Entries are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetPurchaseEntry(string id)
        {
            var purchaseEntryEntity = purchaseEntryServices.GetPurchaseEntry(id);
            if (purchaseEntryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryEntity);
            }
            throw new ApiDataException(1001, "No Purchase Entry found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostPurchaseEntry(PurchaseEntryEntity purchaseEntryEntity)
        {
            var insertedEntity = purchaseEntryServices.CreatePurchaseEntry(purchaseEntryEntity);
            return GetPurchaseEntry(insertedEntity.PurchaseEntryId);
        }

        public HttpResponseMessage PutPurchaseEntry(string id, PurchaseEntryEntity purchaseEntryEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryServices.UpdatePurchaseEntry(id, purchaseEntryEntity));
        }

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