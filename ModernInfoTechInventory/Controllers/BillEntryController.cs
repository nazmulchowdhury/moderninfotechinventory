using System;
using System.Net;
using Model.Sale;
using System.Linq;
using Service.Sale;
using System.Net.Http;
using System.Web.Http;
using Model.Inventory;
using System.Collections.Generic;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Sale;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("billentry")]
    public class BillEntryController : ApiController
    {
        private readonly IBillEntryServices billEntryServices;

        public BillEntryController(IBillEntryServices billEntryServices)
        {
            this.billEntryServices = billEntryServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllBillEntries()
        {
            var billEntryEntities = billEntryServices.GetAllBillEntries();
            if (billEntryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, billEntryEntities);
            }
            throw new ApiDataException(1000, "Bill Entries are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetBillEntry(string id)
        {
            var billEntryEntity = billEntryServices.GetBillEntry(id);
            if (billEntryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, billEntryEntity);
            }
            throw new ApiDataException(1001, "No Bill Entry found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostBillEntry(BillEntryView billEntryView)
        {
            var productQuantities = new HashSet<ProductQuantityEntity>();

            foreach (ProductQuantityView pqv in billEntryView.SaledProducts)
            {
                var productQuantity = new ProductQuantityEntity
                {
                    ProductQuantityId = Guid.NewGuid().ToString(),
                    ProductId = pqv.ProductId,
                    Quantity = pqv.Quantity,
                    Price = pqv.Price
                };
                productQuantities.Add(productQuantity);
            }

            var billEntryEntity = new BillEntryEntity
            {
                BillEntryId = Guid.NewGuid().ToString(),
                CustomerId = billEntryView.CustomerId,
                Discount = billEntryView.Discount,
                ProductQuantities = productQuantities
            };
            
            var insertedEntity = billEntryServices.CreateBillEntry(billEntryEntity);
            return GetBillEntry(insertedEntity.BillEntryId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutBillEntry(string id, BillEntryEntity billEntryEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, billEntryServices.UpdateBillEntry(id, billEntryEntity));
        }

        [Route("{id:length(36)}")]
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