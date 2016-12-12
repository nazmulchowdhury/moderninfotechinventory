using System;
using System.Collections.Generic;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.ViewModels.Requisition
{
    public class RequisitionRequestView
    {
        public RequisitionRequestView()
        {
            this.RequisiteProducts = new HashSet<ProductQuantityView>();
        }

        public DateTime RequisitionDate { get; set; }

        public string Description { get; set; }

        public ICollection<ProductQuantityView> RequisiteProducts { get; set; }
    }

    public class RequisitionApprovalView
    {
        public bool IsApproved { get; set; }
    }
}