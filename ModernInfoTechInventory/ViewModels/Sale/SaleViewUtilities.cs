using System.Collections.Generic;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.ViewModels.Sale
{
    public class BillEntryView
    {
        public BillEntryView()
        {
            this.SaledProducts = new HashSet<ProductQuantityView>();
        }

        public string CustomerId { get; set; }

        public double Discount { get; set; }

        public ICollection<ProductQuantityView> SaledProducts { get; set; }
    }

    public class SaleReturnView
    {
        public SaleReturnView()
        {
            this.SaleReturnedProducts = new HashSet<ProductReturnQuantityView>();
        }

        public string RefInvoiceId { get; set; }

        public double Penalty { get; set; }

        public double PaidAmount { get; set; }

        public ICollection<ProductReturnQuantityView> SaleReturnedProducts { get; set; }
    }
}