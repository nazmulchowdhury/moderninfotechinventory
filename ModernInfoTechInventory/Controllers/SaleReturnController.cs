using System;
using System.Net;
using System.Linq;
using Model.Sale;
using Model.Inventory;
using System.Net.Http;
using System.Web.Http;
using Service.Sale;
using System.Collections.Generic;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Sale;
using ModernInfoTechInventory.ViewModels.Inventory;

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
        public HttpResponseMessage PostSaleReturn(SaleReturnView saleReturnView)
        {

            var productReturnQuantities = new HashSet<ProductReturnQuantityEntity>();

            foreach (ProductReturnQuantityView prqv in saleReturnView.SaleReturnedProducts)
            {
                var productReturnQuantity = new ProductReturnQuantityEntity
                {
                    ProductReturnQuantityId = Guid.NewGuid().ToString(),
                    ProductQuantityId = prqv.ProductQuantityId,
                    ReturnQuantity = prqv.ReturnQuantity
                };
                productReturnQuantities.Add(productReturnQuantity);
            }

            var saleReturnEntity = new SaleReturnEntity
            {
                SaleReturnId = Guid.NewGuid().ToString(),
                RefInvoiceId = saleReturnView.RefInvoiceId,
                Penalty = saleReturnView.Penalty,
                PaidAmount = saleReturnView.PaidAmount,
                ProductReturnQuantities = productReturnQuantities
            };
            var insertedEntity = saleReturnServices.CreateSaleReturn(saleReturnEntity);
            return GetSaleReturn(insertedEntity.SaleReturnId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutSaleReturn(string id, SaleReturnEntity saleReturnEntity)
        {
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