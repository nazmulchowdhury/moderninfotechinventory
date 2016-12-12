using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Service.Sale;
using Model.Sale;
using Model.Inventory;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Sale;
using ModernInfoTechInventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;

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
            var saleReturnEntityMapper = new MapperConfiguration(
                cfg => cfg.CreateMap<SaleReturnView, SaleReturnEntity>()
                    .ConstructUsing((SaleReturnView srv) =>
                    {
                        var sre = new SaleReturnEntity();
                        sre.SaleReturnId = Guid.NewGuid().ToString();

                        foreach (ProductReturnQuantityView prqv in srv.SaleReturnedProducts)
                        {
                            sre.ProductReturnQuantities.Add(new ProductReturnQuantityEntity
                            {
                                ProductReturnQuantityId = Guid.NewGuid().ToString(),
                                ProductQuantityId = prqv.ProductQuantityId,
                                ReturnQuantity = prqv.ReturnQuantity
                            });
                        }
                        return sre;
                    }));

            var saleReturnEntity = saleReturnEntityMapper.CreateMapper().Map<SaleReturnView, SaleReturnEntity>(saleReturnView);
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