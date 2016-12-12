using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.DeliveryOrder;
using Model.DeliveryOrder;
using Model.Inventory;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.DeliveryOrder;
using ModernInfoTechInventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("deliveryorder")]
    public class DeliveryOrderController : ApiController
    {
        private readonly IDeliveryOrderServices deliveryOrderServices;

        public DeliveryOrderController(IDeliveryOrderServices deliveryOrderServices)
        {
            this.deliveryOrderServices = deliveryOrderServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllDeliveryOrders()
        {
            var deliveryOrderEntities = deliveryOrderServices.GetAllDeliveryOrders();
            if (deliveryOrderEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, deliveryOrderEntities);
            }
            throw new ApiDataException(1000, "DeliveryOrders are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetDeliveryOrder(string id)
        {
            var deliveryOrderEntity = deliveryOrderServices.GetDeliveryOrder(id);
            if (deliveryOrderEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, deliveryOrderEntity);
            }
            throw new ApiDataException(1001, "No DeliveryOrder found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostDeliveryOrder(DeliveryOrderView deliveryOrderView)
        {
            var productQuantities = new HashSet<ProductQuantityEntity>();
            foreach (ProductQuantityView pqv in deliveryOrderView.DeliveredProducts)
            {
                productQuantities.Add(new ProductQuantityEntity
                {
                    ProductQuantityId = Guid.NewGuid().ToString(),
                    ProductId = pqv.ProductId,
                    Quantity = pqv.Quantity
                });
            }

            var deliveryOrderEntity = new DeliveryOrderEntity
            {
                DeliveryOrderId = Guid.NewGuid().ToString(),
                RequisitionId = deliveryOrderView.RequisitionId,
                DeliveryOrderDate = deliveryOrderView.DeliveryOrderDate,
                Description = deliveryOrderView.Description,
                ProductQuantities = productQuantities
            };

            var insertedEntity = deliveryOrderServices.CreateDeliveryOrder(deliveryOrderEntity);
            return GetDeliveryOrder(insertedEntity.DeliveryOrderId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutDeliveryOrder(string id, DeliveryStatusView deliveryStatusView)
        {
            var deliveryOrderEntity = deliveryOrderServices.GetDeliveryOrder(id);
            deliveryOrderEntity.IsReceived = deliveryStatusView.IsReceived;
            return Request.CreateResponse(HttpStatusCode.OK, deliveryOrderServices.UpdateDeliveryOrder(id, deliveryOrderEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteDeliveryOrder(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = deliveryOrderServices.DeleteDeliveryOrder(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "DeliveryOrder is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}