using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using Model.Inventory;
using System.Net.Http;
using System.Web.Http;
using Service.Inventory;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("productinfo")]
    public class ProductInfoController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IProductInfoServices productInfoServices;

        public ProductInfoController(IProductInfoServices productInfoServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
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

        [Route("{id:guid}")]
        public HttpResponseMessage GetProduct(string id)
        {
            var productEntity = productInfoServices.GetProduct(id);
            if (productEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, productEntity);
            }
            throw new ApiDataException(1001, "No Product found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("bysubcategory/{id:guid}")]
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
        public HttpResponseMessage PostProduct(ProductInfoEntity productInfoEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            productInfoEntity.ProductId = Guid.NewGuid().ToString();
            productInfoEntity.TenantId = tenantEntity.TenantId;
            productInfoEntity.TenantInfo = tenantEntity;
            var insertedEntity = productInfoServices.CreateProduct(productInfoEntity);
            return GetProduct(insertedEntity.ProductId);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutProduct(string id, ProductInfoEntity productInfoEntity)
        {
            productInfoEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, productInfoServices.UpdateProduct(id, productInfoEntity));
        }

        [Route("{id:guid}")]
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

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateProduct(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var productInfoEntity = productInfoServices.GetProduct(id);
                if (productInfoEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(productInfoEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(productInfoEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Product is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Product has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Product is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}