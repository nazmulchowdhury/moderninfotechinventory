using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Sale;
using Model.Sale;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class BillEntryController : ApiController
    {
        private readonly IBillEntryServices billEntryServices;

        public BillEntryController(IBillEntryServices billEntryServices)
        {
            this.billEntryServices = billEntryServices;
        }

        public HttpResponseMessage GetAllBillEntries()
        {
            var billEntryEntities = billEntryServices.GetAllBillEntries().ToList();
            if (billEntryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, billEntryEntities);
            }
            throw new ApiDataException(1000, "Bill Entries are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetBillEntry(string id)
        {
            var billEntryEntity = billEntryServices.GetBillEntry(id);
            if (billEntryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, billEntryEntity);
            }
            throw new ApiDataException(1001, "No Bill Entry found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostBillEntry(BillEntryEntity billEntryEntity)
        {
            var insertedEntity = billEntryServices.CreateBillEntry(billEntryEntity);
            return GetBillEntry(insertedEntity.BillEntryId);
        }

        public HttpResponseMessage PutBillEntry(string id, BillEntryEntity billEntry)
        {
            return Request.CreateResponse(HttpStatusCode.OK, billEntryServices.UpdateBillEntry(id, billEntry));
        }

        public HttpResponseMessage DeleteBillEntry(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = billEntryServices.DeleteBillEntry(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Bill Entry is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}