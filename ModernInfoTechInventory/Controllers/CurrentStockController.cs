using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Model.Utilities;
using Service.Utilities;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("currentstock")]
    public class CurrentStockController : ApiController
    {
        private readonly ICurrentStockServices currentStockServices;

        public CurrentStockController(ICurrentStockServices currentStockServices)
        {
            this.currentStockServices = currentStockServices;
        }

        [Route("product/{option:int}/{id:guid}")]
        public HttpResponseMessage GetAllProducts(string id, Option option)
        {
            if (option == Option.SALE_RETURN || option == Option.PURCHASE_RETURN)
            {
                var productReturnQuantities = currentStockServices.GetAllStockedProductReturns(id, option);
                if (productReturnQuantities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, productReturnQuantities);
                }
                throw new ApiDataException(1000, "Products are not found for this id: " + id, HttpStatusCode.NotFound);
            }
            else
            {
                var productQuantities = currentStockServices.GetAllStockedProducts(id, option);
                if (productQuantities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, productQuantities);
                }
                throw new ApiDataException(1000, "Products are not found for this id: " + id, HttpStatusCode.NotFound);
            }
        }

        [Route("{id:guid}")]
        public HttpResponseMessage GetCurrentStockedProducts(string id)
        {
            var currentStock = currentStockServices.GetCurrentStock(id);
            if (currentStock != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, currentStock);
            }
            throw new ApiDataException(1001, "This product id: " + id + " is not  exist in the current stock", HttpStatusCode.NotFound);
        }
    }
}