using Model.DeliveryOrder;
using System.Collections.Generic;

namespace Service.DeliveryOrder
{
    public interface IDeliveryOrderServices
    {
        ICollection<DeliveryOrderEntity> GetAllDeliveryOrders();
        DeliveryOrderEntity GetDeliveryOrder(string deliveryOrderId);
        DeliveryOrderEntity GetDeliveryOrderByRequisitionId(string requisitionId);
        DeliveryOrderEntity CreateDeliveryOrder(DeliveryOrderEntity deliveryOrderEntity);
        bool UpdateDeliveryOrder(string deliveryOrderId, DeliveryOrderEntity deliveryOrderEntity);
        bool DeleteDeliveryOrder(string deliveryOrderId);
    }
}
