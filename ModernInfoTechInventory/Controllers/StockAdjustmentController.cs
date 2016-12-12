using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Model.Inventory;
using Service.Inventory;
using System.Collections.Generic;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("stockadjustment")]
    public class StockAdjustmentController : ApiController
    {
        private readonly IStockAdjustmentServices stockAdjustmentServices;

        public StockAdjustmentController(IStockAdjustmentServices stockAdjustmentServices)
        {
            this.stockAdjustmentServices = stockAdjustmentServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllStockAdjustments()
        {
            var stockAdjustmentEntities = stockAdjustmentServices.GetAllStockAdjustments();
            if (stockAdjustmentEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, stockAdjustmentEntities);
            }
            throw new ApiDataException(1000, "Stock Adjustments are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetStockAdjustment(string id)
        {
            var stockAdjustmentEntity = stockAdjustmentServices.GetStockAdjustment(id);
            if (stockAdjustmentEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, stockAdjustmentEntity);
            }
            throw new ApiDataException(1001, "No Stock Adjustment found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostStockAdjustment(StockAdjustmentView stockAdjustmentView)
        {
            var productQuantities = new HashSet<ProductQuantityEntity>();
            foreach (ProductQuantityView pqv in stockAdjustmentView.ProductQuantities)
            {
                productQuantities.Add(new ProductQuantityEntity
                {
                    ProductQuantityId = Guid.NewGuid().ToString(),
                    ProductId = pqv.ProductId,
                    Quantity = pqv.Quantity
                });
            }

            var stockAdjustmentEntity = new StockAdjustmentEntity
            {
                StockAdjustmentId = Guid.NewGuid().ToString(),
                ReceiveDate = stockAdjustmentView.ReceiveDate,
                ReceiveNumber = stockAdjustmentView.ReceiveNumber,
                ProductQuantities = productQuantities
            };

            var insertedEntity = stockAdjustmentServices.CreateStockAdjustment(stockAdjustmentEntity);
            return GetStockAdjustment(insertedEntity.StockAdjustmentId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutStockAdjustment(string id, StockAdjustmentEntity stockAdjustmentEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, stockAdjustmentServices.UpdateStockAdjustment(id, stockAdjustmentEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeletestockAdjustment(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = stockAdjustmentServices.DeleteStockAdjustment(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Stock Adjustment is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}