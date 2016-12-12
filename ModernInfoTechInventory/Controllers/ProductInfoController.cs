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
    [RoutePrefix("productinfo")]
    public class ProductInfoController : ApiController
    {
        private readonly IProductInfoServices productInfoServices;

        public ProductInfoController(IProductInfoServices productInfoServices)
        {
            this.productInfoServices = productInfoServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllProducts()
        {
            var productEntities = productInfoServices.GetAllProducts();
            if (productEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            }
            throw new ApiDataException(1000, "Products are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetProduct(string id)
        {
            var productEntity = productInfoServices.GetProduct(id);
            if (productEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, productEntity);
            }
            throw new ApiDataException(1001, "No Product found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("bysubcategory/{id:length(36)}")]
        public HttpResponseMessage GetAllProducts(string id)
        {
            var productEntities = productInfoServices.GetAllProducts(id);
            if (productEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            }
            throw new ApiDataException(1000, "Products are not found for this sub category id " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostProduct(ProductInfoView productInfoView)
        {
            var productInfoEntity = new ProductInfoEntity
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductName = productInfoView.ProductName,
                Barcode = productInfoView.Barcode,
                CostPrice = productInfoView.CostPrice,
                SalePrice = productInfoView.SalePrice,
                ReorderLevel = productInfoView.ReorderLevel,
                SubCategoryId = productInfoView.SubCategoryId
            };
            var insertedEntity = productInfoServices.CreateProduct(productInfoEntity);
            return GetProduct(insertedEntity.ProductId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutProduct(string id, ProductInfoEntity productInfoEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, productInfoServices.UpdateProduct(id, productInfoEntity));
        }

        [Route("{id:length(36)}")]
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