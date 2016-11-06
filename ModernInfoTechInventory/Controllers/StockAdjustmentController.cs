using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Product;
using Model.Product;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class StockAdjustmentController : ApiController
    {
        private readonly IStockAdjustmentServices stockAdjustmentServices;

        public StockAdjustmentController(IStockAdjustmentServices stockAdjustmentServices)
        {
            this.stockAdjustmentServices = stockAdjustmentServices;
        }

        public HttpResponseMessage GetAllStockAdjustments()
        {
            var stockAdjustmentEntities = stockAdjustmentServices.GetAllStockAdjustments().ToList();
            if (stockAdjustmentEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, stockAdjustmentEntities);
            }
            throw new ApiDataException(1000, "Stock Adjustments are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetStockAdjustment(string id)
        {
            var stockAdjustmentEntity = stockAdjustmentServices.GetStockAdjustment(id);
            if (stockAdjustmentEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, stockAdjustmentEntity);
            }
            throw new ApiDataException(1001, "No Stock Adjustment found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostStockAdjustment(StockAdjustmentEntity stockAdjustmentEntity)
        {
            var insertedEntity = stockAdjustmentServices.CreateStockAdjustment(stockAdjustmentEntity);
            return GetStockAdjustment(insertedEntity.StockAdjustmentId);
        }

        public HttpResponseMessage PutStockAdjustment(string id, StockAdjustmentEntity stockAdjustmentEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, stockAdjustmentServices.UpdateStockAdjustment(id, stockAdjustmentEntity));
        }

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