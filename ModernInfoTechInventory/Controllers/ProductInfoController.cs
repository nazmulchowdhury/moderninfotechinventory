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
    public class ProductInfoController : ApiController
    {
        private readonly IProductInfoServices productInfoServices;

        public ProductInfoController(IProductInfoServices productInfoServices)
        {
            this.productInfoServices = productInfoServices;
        }

        public HttpResponseMessage GetAllProducts()
        {
            var productEntities = productInfoServices.GetAllProducts().ToList();
            if (productEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            }
            throw new ApiDataException(1000, "Products are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetProduct(string id)
        {
            var productEntity = productInfoServices.GetProduct(id);
            if (productEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, productEntity);
            }
            throw new ApiDataException(1001, "No Product found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostProduct(ProductInfoEntity productInfoEntity)
        {
            var insertedEntity = productInfoServices.CreateProduct(productInfoEntity);
            return GetProduct(insertedEntity.ProductId);
        }

        public HttpResponseMessage PutProduct(string id, ProductInfoEntity productInfoEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, productInfoServices.UpdateProduct(id, productInfoEntity));
        }

        public HttpResponseMessage DeleteProduct(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = productInfoServices.DeleteProduct(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Product is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}