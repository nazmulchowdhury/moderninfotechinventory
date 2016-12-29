using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using System.Net.Http;
using System.Web.Http;
using Model.Inventory;
using Model.DeliveryOrder;
using Service.DeliveryOrder;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ViewModels.DeliveryOrder;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("deliveryorder")]
    public class DeliveryOrderController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IDeliveryOrderServices deliveryOrderServices;

        public DeliveryOrderController(IDeliveryOrderServices deliveryOrderServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
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

        [Route("{id:guid}")]
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

        [Route("{id:guid}")]
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

        [Route("{id:guid}")]
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

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateDeliveryOrder(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var deliveryOrderEntity = deliveryOrderServices.GetDeliveryOrder(id);
                if (deliveryOrderEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(deliveryOrderEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(deliveryOrderEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "DeliveryOrder is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "DeliveryOrder has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "DeliveryOrder is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}