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
        public HttpResponseMessage PostStockAdjustment(StockAdjustmentEntity stockAdjustmentEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            stockAdjustmentEntity.StockAdjustmentId = Guid.NewGuid().ToString();
            stockAdjustmentEntity.ProductQuantities = stockAdjustmentEntity.ProductQuantities.Select(productQuantity =>
            {
                productQuantity.ProductQuantityId = Guid.NewGuid().ToString();
                productQuantity.TenantId = tenantEntity.TenantId;
                return productQuantity;
            }).ToList();
            stockAdjustmentEntity.TenantId = tenantEntity.TenantId;
            stockAdjustmentEntity.TenantInfo = tenantEntity;
            
            var insertedEntity = stockAdjustmentServices.CreateStockAdjustment(stockAdjustmentEntity);
            return GetStockAdjustment(insertedEntity.StockAdjustmentId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutStockAdjustment(string id, StockAdjustmentEntity stockAdjustmentEntity)
        {
            stockAdjustmentEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
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