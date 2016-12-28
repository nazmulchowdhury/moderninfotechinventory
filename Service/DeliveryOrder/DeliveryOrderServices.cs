using Model.DeliveryOrder;
using System.Collections.Generic;
using Data.Repositories.DeliveryOrder;

namespace Service.DeliveryOrder
{
    public class DeliveryOrderServices : IDeliveryOrderServices
    {
        private readonly IDeliveryOrderRepository deliveryOrderRepository;

        public DeliveryOrderServices(IDeliveryOrderRepository deliveryOrderRepository)
        {
            this.deliveryOrderRepository = deliveryOrderRepository;
        }

        public ICollection<DeliveryOrderEntity> GetAllDeliveryOrders()
        {
            return deliveryOrderRepository.GetAll();
        }

        public DeliveryOrderEntity GetDeliveryOrder(string deliveryOrderId)
        {
            return deliveryOrderRepository.GetById(deliveryOrderId);
        }

        public DeliveryOrderEntity GetDeliveryOrderByRequisitionId(string requisitionId)
        {
            return deliveryOrderRepository.Get(deliveryorder => deliveryorder.RequisitionId == requisitionId);
        }

        public DeliveryOrderEntity CreateDeliveryOrder(DeliveryOrderEntity deliveryOrderEntity)
        {
            return deliveryOrderRepository.Add(deliveryOrderEntity);
        }

        public bool UpdateDeliveryOrder(string deliveryOrderId, DeliveryOrderEntity deliveryOrderEntity)
        {
            var storedItem = deliveryOrderRepository.GetById(deliveryOrderId);

            if (storedItem != null)
            {
                storedItem.RequisitionId = deliveryOrderEntity.RequisitionId;
                storedItem.DeliveryOrderDate = deliveryOrderEntity.DeliveryOrderDate;
                storedItem.IsReceived = deliveryOrderEntity.IsReceived;
                storedItem.Description = deliveryOrderEntity.Description;
                storedItem.TenantInfo.UserId = deliveryOrderEntity.TenantInfo.UserId;

                deliveryOrderRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDeliveryOrder(string deliveryOrderId)
        {
            return deliveryOrderRepository.Delete(deliveryOrderId);
        }
    }
}
