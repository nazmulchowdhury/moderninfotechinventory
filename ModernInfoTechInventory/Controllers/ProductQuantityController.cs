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
    [RoutePrefix("productquantity")]
    public class ProductQuantityController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IProductQuantityServices productQuantityServices;

        public ProductQuantityController(IProductQuantityServices productQuantityServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
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

        [Route("{id:guid}")]
        public HttpResponseMessage GetProductQuantity(string id)
        {
            var productQuantityEntity = productQuantityServices.GetProductQuantity(id);
            if (productQuantityEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, productQuantityEntity);
            }
            throw new ApiDataException(1001, "No Product Quantity found for this id: " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostProductQuantity(ProductQuantityEntity productQuantityEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            productQuantityEntity.ProductQuantityId = Guid.NewGuid().ToString();
            productQuantityEntity.Price = null;
            productQuantityEntity.TenantId = tenantEntity.TenantId;
            productQuantityEntity.TenantInfo = tenantEntity;
            var insertedEntity = productQuantityServices.CreateProductQuantity(productQuantityEntity);
            return GetProductQuantity(insertedEntity.ProductQuantityId);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutProductQuantity(string id, ProductQuantityEntity productQuantityEntity)
        {
            productQuantityEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, productQuantityServices.UpdateProductQuantity(id, productQuantityEntity));
        }

        [Route("{id:guid}")]
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

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateProductQuantity(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var productQuantityEntity = productQuantityServices.GetProductQuantity(id);
                if (productQuantityEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(productQuantityEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(productQuantityEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Product Quantity is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Product Quantity has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Product Quantity is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}