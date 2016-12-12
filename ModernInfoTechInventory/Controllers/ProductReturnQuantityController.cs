using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Inventory;
using Model.Inventory;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("productreturnquantity")]
    public class ProductReturnQuantityController : ApiController
    {
        private readonly IProductReturnQuantityServices productReturnQuantityServices;

        public ProductReturnQuantityController(IProductReturnQuantityServices productReturnQuantityServices)
        {
            this.productReturnQuantityServices = productReturnQuantityServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllProductReturnQuantities()
        {
            var productReturnQuantityEntities = productReturnQuantityServices.GetAllProductReturnQuantities();
            if (productReturnQuantityEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, productReturnQuantityEntities);
            }
            throw new ApiDataException(1000, "Product Return Quantities are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetProductReturnQuantity(string id)
        {
            var productReturnQuantityEntity = productReturnQuantityServices.GetProductReturnQuantity(id);
            if (productReturnQuantityEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, productReturnQuantityEntity);
            }
            throw new ApiDataException(1001, "No Product Return Quantity found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostProductReturnQuantity(ProductReturnQuantityView productReturnQuantityView)
        {
            var productReturnQuantityEntity = new ProductReturnQuantityEntity
            {
                ProductReturnQuantityId = Guid.NewGuid().ToString(),
                ProductQuantityId = productReturnQuantityView.ProductQuantityId,
                ReturnQuantity = productReturnQuantityView.ReturnQuantity
            };
            var insertedEntity = productReturnQuantityServices.CreateProductReturnQuantity(productReturnQuantityEntity);
            return GetProductReturnQuantity(insertedEntity.ProductReturnQuantityId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutProductReturnQuantity(string id, ProductReturnQuantityEntity productReturnQuantityEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, productReturnQuantityServices.UpdateProductReturnQuantity(id, productReturnQuantityEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteProductReturnQuantity(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = productReturnQuantityServices.DeleteProductReturnQuantity(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Product Return Quantity is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}