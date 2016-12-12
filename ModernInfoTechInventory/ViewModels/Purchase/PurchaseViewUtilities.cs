using System;
using System.Collections.Generic;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.ViewModels.Purchase
{
    public class PurchaseEntryView
    {
        public PurchaseEntryView()
        {
            this.PurchasedProducts = new HashSet<ProductQuantityView>();
        }
        public ICollection<ProductQuantityView> PurchasedProducts { get; set; }

        public string SupplierId { get; set; }

        public DateTime ReceiveDate { get; set; }

        public string ReceiveNumber { get; set; }

        public double PaidAmount { get; set; }
    }

    public class PurchaseReturnView
    {
        public PurchaseReturnView()
        {
            this.PurchaseReturnedProducts = new HashSet<ProductReturnQuantityView>();
        }

        public string RefInvoiceId { get; set; }

        public DateTime ReturnDate { get; set; }

        public double Penalty { get; set; }

        public double PaidAmount { get; set; }

        public ICollection<ProductReturnQuantityView> PurchaseReturnedProducts { get; set; }
    }
}