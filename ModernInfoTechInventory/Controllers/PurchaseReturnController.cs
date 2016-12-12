using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using AutoMapper;
using Service.Purchase;
using Model.Purchase;
using Model.Inventory;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Purchase;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("purchasereturn")]
    public class PurchaseReturnController : ApiController
    {
        private readonly IPurchaseReturnServices purchaseReturnServices;

        public PurchaseReturnController(IPurchaseReturnServices purchaseReturnServices)
        {
            this.purchaseReturnServices = purchaseReturnServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllPurchaseReturns()
        {
            var purchaseReturnsEntities = purchaseReturnServices.GetAllPurchaseReturns();
            if (purchaseReturnsEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseReturnsEntities);
            }
            throw new ApiDataException(1000, "Purchase Returns are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetPurchaseReturn(string id)
        {
            var purchaseReturnEntity = purchaseReturnServices.GetPurchaseReturn(id);
            if (purchaseReturnEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseReturnEntity);
            }
            throw new ApiDataException(1001, "No Purchase Return found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostPurchaseReturn(PurchaseReturnView purchaseReturnView)
        {
            var purchaseReturnEntityMapper = new MapperConfiguration(
                cfg => cfg.CreateMap<PurchaseReturnView, PurchaseReturnEntity>()
                    .ConstructUsing((PurchaseReturnView prv) =>
                    {
                        var pre = new PurchaseReturnEntity();
                        pre.PurchaseReturnId = Guid.NewGuid().ToString();

                        foreach (ProductReturnQuantityView prqv in prv.PurchaseReturnedProducts)
                        {
                            pre.ProductReturnQuantities.Add(new ProductReturnQuantityEntity
                            {
                                ProductReturnQuantityId = Guid.NewGuid().ToString(),
                                ProductQuantityId = prqv.ProductQuantityId,
                                ReturnQuantity = prqv.ReturnQuantity
                            });
                        }
                        return pre;
                    }));

            var purchaseReturnEntity = purchaseReturnEntityMapper.CreateMapper().Map<PurchaseReturnView, PurchaseReturnEntity>(purchaseReturnView);
            var insertedEntity = purchaseReturnServices.CreatePurchaseReturn(purchaseReturnEntity);
            return GetPurchaseReturn(insertedEntity.PurchaseReturnId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutPurchaseReturn(string id, PurchaseReturnEntity purchaseReturnEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, purchaseReturnServices.UpdatePurchaseReturn(id, purchaseReturnEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeletePurchaseReturn(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = purchaseReturnServices.DeletePurchaseReturn(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Purchase Return is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}