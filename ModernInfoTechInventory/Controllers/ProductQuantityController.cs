using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Inventory;
using Model.Inventory;
using ModernInfoTechInventory.ViewModels.Inventory;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("productquantity")]
    public class ProductQuantityController : ApiController
    {
        private readonly IProductQuantityServices productQuantityServices;

        public ProductQuantityController(IProductQuantityServices productQuantityServices)
        {
            this.productQuantityServices = productQuantityServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllProductQuantities()
        {
            var productQuantityEntities = productQuantityServices.GetAllProductQuantities();
            if (productQuantityEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, productQuantityEntities);
            }
            throw new ApiDataException(1000, "Product Quantities are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetProductQuantity(string id)
        {
            var productQuantityEntity = productQuantityServices.GetProductQuantity(id);
            if (productQuantityEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, productQuantityEntity);
            }
            throw new ApiDataException(1001, "No Product Quantity found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostProductQuantity(ProductQuantityView productQuantityView)
        {
            var productQuantityEntity = new ProductQuantityEntity
            {
                ProductQuantityId = Guid.NewGuid().ToString(),
                ProductId = productQuantityView.ProductId,
                Quantity = productQuantityView.Quantity
            };
            var insertedEntity = productQuantityServices.CreateProductQuantity(productQuantityEntity);
            return GetProductQuantity(insertedEntity.ProductQuantityId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutProductQuantity(string id, ProductQuantityEntity productQuantityEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, productQuantityServices.UpdateProductQuantity(id, productQuantityEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteProductQuantity(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = productQuantityServices.DeleteProductQuantity(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Product Quantity is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}