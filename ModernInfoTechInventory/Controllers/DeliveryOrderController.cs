using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Model.Inventory;
using Model.BaseModel;
using Model.DeliveryOrder;
using Service.DeliveryOrder;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ViewModels.DeliveryOrder;

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
        public HttpResponseMessage PostDeliveryOrder(DeliveryOrderEntity deliveryOrderEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            deliveryOrderEntity.DeliveryOrderId = Guid.NewGuid().ToString();
            deliveryOrderEntity.IsReceived = false;
            deliveryOrderEntity.TenantId = tenantEntity.TenantId;
            deliveryOrderEntity.TenantInfo = tenantEntity;
            deliveryOrderEntity.ProductQuantities = deliveryOrderEntity.ProductQuantities.Select(productQuantity =>
            {
                productQuantity.ProductQuantityId = Guid.NewGuid().ToString();
                productQuantity.TenantId = tenantEntity.TenantId;
                return productQuantity;
            }).ToList();

            var insertedEntity = deliveryOrderServices.CreateDeliveryOrder(deliveryOrderEntity);
            return GetDeliveryOrder(insertedEntity.DeliveryOrderId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutDeliveryOrder(string id, DeliveryStatusView deliveryStatusView)
        {
            var deliveryOrderEntity = deliveryOrderServices.GetDeliveryOrder(id);
            deliveryOrderEntity.IsReceived = deliveryStatusView.IsReceived;
            deliveryOrderEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
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