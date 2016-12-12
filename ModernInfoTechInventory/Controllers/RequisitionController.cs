using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Requisition;
using Service.DeliveryOrder;
using Model.Requisition;
using Model.Inventory;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Requisition;
using ModernInfoTechInventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("requisition")]
    public class RequisitionController : ApiController
    {
        private readonly IRequisitionServices requisitionServices;
        private readonly IDeliveryOrderServices deliveryOrderServices;

        public RequisitionController(IRequisitionServices requisitionServices, IDeliveryOrderServices deliveryOrderServices)
        {
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
        public HttpResponseMessage PostRequisition(RequisitionRequestView requisitionRequestView)
        {
            var productQuantities = new HashSet<ProductQuantityEntity>();
            foreach (ProductQuantityView pqv in requisitionRequestView.RequisiteProducts)
            {
                productQuantities.Add(new ProductQuantityEntity
                {
                    ProductQuantityId = Guid.NewGuid().ToString(),
                    ProductId = pqv.ProductId,
                    Quantity = pqv.Quantity
                });
            }

            var requisitionEntity = new RequisitionEntity
            {
                RequisitionId = Guid.NewGuid().ToString(),
                RequisitionDate = requisitionRequestView.RequisitionDate,
                Description = requisitionRequestView.Description,
                ProductQuantities = productQuantities
            };

            var insertedEntity = requisitionServices.CreateRequisition(requisitionEntity);
            return GetRequisition(insertedEntity.RequisitionId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutRequisition(string id, RequisitionApprovalView requisitionApprovalView)
        {
            var requisitionEntity = requisitionServices.GetRequisition(id);
            requisitionEntity.IsApproved = requisitionApprovalView.IsApproved;
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
    }
}