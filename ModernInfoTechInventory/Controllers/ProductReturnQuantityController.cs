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
    [RoutePrefix("productreturnquantity")]
    public class ProductReturnQuantityController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IProductReturnQuantityServices productReturnQuantityServices;

        public ProductReturnQuantityController(IProductReturnQuantityServices productReturnQuantityServices,
            ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
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

        [Route("{id:guid}")]
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
        public HttpResponseMessage PostProductReturnQuantity(ProductReturnQuantityEntity productReturnQuantityEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            productReturnQuantityEntity.ProductReturnQuantityId = Guid.NewGuid().ToString();
            productReturnQuantityEntity.TenantId = tenantEntity.TenantId;
            productReturnQuantityEntity.TenantInfo = tenantEntity;
            var insertedEntity = productReturnQuantityServices.CreateProductReturnQuantity(productReturnQuantityEntity);
            return GetProductReturnQuantity(insertedEntity.ProductReturnQuantityId);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutProductReturnQuantity(string id, ProductReturnQuantityEntity productReturnQuantityEntity)
        {
            productReturnQuantityEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, productReturnQuantityServices.UpdateProductReturnQuantity(id, productReturnQuantityEntity));
        }

        [Route("{id:guid}")]
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

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateProductReturnQuantity(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var productReturnQuantityEntity = productReturnQuantityServices.GetProductReturnQuantity(id);
                if (productReturnQuantityEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(productReturnQuantityEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(productReturnQuantityEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Product Return Quantity is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Product Return Quantity has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Product Return Quantity is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}