using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using AutoMapper;
using Model.Inventory;
using Model.Purchase;
using Service.Purchase;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Purchase;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("purchaseentry")]
    public class PurchaseEntryController : ApiController
    {
        private readonly IPurchaseEntryServices purchaseEntryServices;

        public PurchaseEntryController(IPurchaseEntryServices purchaseEntryServices)
        {
            this.purchaseEntryServices = purchaseEntryServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllPurchaseEntries()
        {
            var purchaseEntryEntities = purchaseEntryServices.GetAllPurchaseEntries();
            if (purchaseEntryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryEntities);
            }
            throw new ApiDataException(1000, "Purchase Entries are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetPurchaseEntry(string id)
        {
            var purchaseEntryEntity = purchaseEntryServices.GetPurchaseEntry(id);
            if (purchaseEntryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryEntity);
            }
            throw new ApiDataException(1001, "No Purchase Entry found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostPurchaseEntry(PurchaseEntryView purchaseEntryView)
        {
            var purchaseEntryEntityMapper = new MapperConfiguration(
                cfg => cfg.CreateMap<PurchaseEntryView, PurchaseEntryEntity>()
                    .ConstructUsing((PurchaseEntryView pev) =>
                    {
                        var pe = new PurchaseEntryEntity();
                        pe.PurchaseEntryId = Guid.NewGuid().ToString();

                        foreach (ProductQuantityView product in pev.PurchasedProducts)
                        {
                            pe.ProductQuantities.Add(new ProductQuantityEntity
                            {
                                ProductQuantityId = Guid.NewGuid().ToString(),
                                ProductId = product.ProductId,
                                Quantity = product.Quantity
                            });
                        }
                        return pe;
                    }));

            var purchaseEntryEntity = purchaseEntryEntityMapper.CreateMapper().Map<PurchaseEntryView, PurchaseEntryEntity>(purchaseEntryView);
            var insertedEntity = purchaseEntryServices.CreatePurchaseEntry(purchaseEntryEntity);
            return GetPurchaseEntry(insertedEntity.PurchaseEntryId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutPurchaseEntry(string id, PurchaseEntryEntity purchaseEntryEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, purchaseEntryServices.UpdatePurchaseEntry(id, purchaseEntryEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeletePurchaseEntry(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = purchaseEntryServices.DeletePurchaseEntry(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Purchase Entry is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}