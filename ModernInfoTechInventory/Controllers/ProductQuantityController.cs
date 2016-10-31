using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Product.ProductInfo;
using Model.Product;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class ProductQuantityController : ApiController
    {
        private readonly IProductQuantityServices productQuantityServices;

        public ProductQuantityController(IProductQuantityServices productQuantityServices)
        {
            this.productQuantityServices = productQuantityServices;
        }

        public HttpResponseMessage GetAllProductQuantities()
        {
            var productQuantityEntities = productQuantityServices.GetAllProductQuantities().ToList();
            if (productQuantityEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, productQuantityEntities);
            }
            throw new ApiDataException(1000, "Product Quantities are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetProductQuantity(string id)
        {
            var productEntity = productQuantityServices.GetProductQuantity(id);
            if (productEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, productEntity);
            }
            throw new ApiDataException(1001, "No Product Quantity found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostProductQuantity(ProductQuantityEntity productQuantityEntity)
        {
            var insertedEntity = productQuantityServices.CreateProductQuantity(productQuantityEntity);
            return GetProductQuantity(insertedEntity.ProductQuantityId);
        }

        public HttpResponseMessage PutProductQuantity(string id, ProductQuantityEntity productQuantityEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, productQuantityServices.UpdateProductQuantity(id, productQuantityEntity));
        }

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