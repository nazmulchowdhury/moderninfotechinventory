using System;
using Model.Sale;
using System.Net;
using System.Linq;
using Service.Sale;
using Model.Inventory;
using Model.BaseModel;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("salereturn")]
    public class SaleReturnController : ApiController
    {
        private readonly ISaleReturnServices saleReturnServices;

        public SaleReturnController(ISaleReturnServices saleReturnServices)
        {
            this.saleReturnServices = saleReturnServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllSaleReturns()
        {
            var saleReturnEntities = saleReturnServices.GetAllSaleReturns();
            if (saleReturnEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, saleReturnEntities);
            }
            throw new ApiDataException(1000, "Sale Returns are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetSaleReturn(string id)
        {
            var saleReturnEntity = saleReturnServices.GetSaleReturn(id);
            if (saleReturnEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, saleReturnEntity);
            }
            throw new ApiDataException(1001, "No Sale Return found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostSaleReturn(SaleReturnEntity saleReturnEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            saleReturnEntity.SaleReturnId = Guid.NewGuid().ToString();
            saleReturnEntity.TenantId = tenantEntity.TenantId;
            saleReturnEntity.TenantInfo = tenantEntity;
            saleReturnEntity.SaleReturnedProducts = saleReturnEntity.SaleReturnedProducts.Select(productReturnQuantity =>
            {
                productReturnQuantity.ProductReturnQuantityId = Guid.NewGuid().ToString();
                productReturnQuantity.TenantId = tenantEntity.TenantId;
                return productReturnQuantity;
            }).ToList();

            var insertedEntity = saleReturnServices.CreateSaleReturn(saleReturnEntity);
            return GetSaleReturn(insertedEntity.SaleReturnId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutSaleReturn(string id, SaleReturnEntity saleReturnEntity)
        {
            saleReturnEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, saleReturnServices.UpdateSaleReturn(id, saleReturnEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteSaleReturn(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = saleReturnServices.DeleteSaleReturn(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Sale Return is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}