using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using System.Net.Http;
using System.Web.Http;
using Model.Inventory;
using Model.Requisition;
using Service.Requisition;
using Service.DeliveryOrder;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ViewModels.Requisition;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("requisition")]
    public class RequisitionController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IRequisitionServices requisitionServices;
        private readonly IDeliveryOrderServices deliveryOrderServices;

        public RequisitionController(IRequisitionServices requisitionServices,
            IDeliveryOrderServices deliveryOrderServices,
            ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.requisitionServices = requisitionServices;
            this.deliveryOrderServices = deliveryOrderServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllRequisitions()
        {
            var requisitionEntities = requisitionServices.GetAllRequisitions();
            if (requisitionEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, requisitionEntities);
            }
            throw new ApiDataException(1000, "Requisitions are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetRequisition(string id)
        {
            var requisitionEntity = requisitionServices.GetRequisition(id);
            if (requisitionEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, requisitionEntity);
            }
            throw new ApiDataException(1001, "No Requisition found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostRequisition(RequisitionEntity requisitionEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            requisitionEntity.RequisitionId = Guid.NewGuid().ToString();
            requisitionEntity.IsApproved = false;
            requisitionEntity.TenantId = tenantEntity.TenantId;
            requisitionEntity.TenantInfo = tenantEntity;
            requisitionEntity.ProductQuantities = requisitionEntity.ProductQuantities.Select(productQuantity =>
            {
                productQuantity.ProductQuantityId = Guid.NewGuid().ToString();
                productQuantity.TenantId = tenantEntity.TenantId;
                return productQuantity;
            }).ToList();

            var insertedEntity = requisitionServices.CreateRequisition(requisitionEntity);
            return GetRequisition(insertedEntity.RequisitionId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutRequisition(string id, RequisitionApprovalView requisitionApprovalView)
        {
            var requisitionEntity = requisitionServices.GetRequisition(id);
            requisitionEntity.IsApproved = requisitionApprovalView.IsApproved;
            requisitionEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, requisitionServices.UpdateRequisition(id, requisitionEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteRequisition(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var eliminatedDeliverOrder = deliveryOrderServices.GetDeliveryOrderByRequisitionId(id);
                if (eliminatedDeliverOrder != null)
                {
                    deliveryOrderServices.DeleteDeliveryOrder(eliminatedDeliverOrder.DeliveryOrderId);
                }

                var isSuccess = requisitionServices.DeleteRequisition(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Requisition is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:length(36)}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateRequisition(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var requisitionEntity = requisitionServices.GetRequisition(id);
                if (requisitionEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(requisitionEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(requisitionEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Requisition is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Requisition has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Requisition is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}