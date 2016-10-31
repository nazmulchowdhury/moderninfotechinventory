using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Product.SaleReturn;
using Model.Product;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class SaleReturnQuantityController : ApiController
    {
        private readonly ISaleReturnQuantityServices saleReturnQuantityServices;

        public SaleReturnQuantityController(SaleReturnQuantityServices saleReturnQuantityServices)
        {
            this.saleReturnQuantityServices = saleReturnQuantityServices;
        }

        public HttpResponseMessage GetAllSaleReturnQuantities()
        {
            var saleReturnQuantityEntities = saleReturnQuantityServices.GetAllSaleReturnQuantities().ToList();
            if (saleReturnQuantityEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, saleReturnQuantityEntities);
            }
            throw new ApiDataException(1000, "Sale Return Quantities are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetSaleReturnQuantity(string id)
        {
            var productEntity = saleReturnQuantityServices.GetSaleReturnQuantity(id);
            if (productEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, productEntity);
            }
            throw new ApiDataException(1001, "No Sale Return Quantity found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostSaleReturnQuantity(SaleReturnQuantityEntity saleReturnQuantityEntity)
        {
            var insertedEntity = saleReturnQuantityServices.CreateSaleReturnQuantity(saleReturnQuantityEntity);
            return GetSaleReturnQuantity(insertedEntity.SaleReturnQuantityId);
        }

        public HttpResponseMessage PutSaleReturnQuantity(string id, SaleReturnQuantityEntity saleReturnQuantityEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, saleReturnQuantityServices.UpdateSaleReturnQuantity(id, saleReturnQuantityEntity));
        }

        public HttpResponseMessage DeleteSaleReturnQuantity(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = saleReturnQuantityServices.DeleteSaleReturnQuantity(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Sale Return Quantity is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}