using System;
using System.Collections.Generic;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.ViewModels.DeliveryOrder
{
    public class DeliveryOrderView
    {
        public DeliveryOrderView()
        {
            this.DeliveredProducts = new HashSet<ProductQuantityView>();
        }

        public string RequisitionId { get; set; }

        public DateTime DeliveryOrderDate { get; set; }

        public string Description { get; set; }

        public ICollection<ProductQuantityView> DeliveredProducts { get; set; }
    }

    public class DeliveryStatusView
    {
        public bool IsReceived { get; set; }
    }
}