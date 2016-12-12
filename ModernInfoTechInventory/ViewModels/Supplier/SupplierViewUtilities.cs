using System;

namespace ModernInfoTechInventory.ViewModels.Supplier
{
    public class SupplierView : IEquatable<SupplierView>
    {
        public string SupplierName { get; set; }

        public string LocationId { get; set; }
        
        public string PhoneNumber { get; set; }

        public override int GetHashCode()
        {
            return PhoneNumber.GetHashCode();
        }

        public bool Equals(SupplierView other)
        {
            return this.PhoneNumber.Equals(other.PhoneNumber);
        }
    }

    public class SupplierPaymentView : IEquatable<SupplierPaymentView>
    {
        public DateTime PaymentDate { get; set; }

        public double PaidAmount { get; set; }

        public string Description { get; set; }

        public string SupplierId { get; set; }

        public override int GetHashCode()
        {
            return SupplierId.GetHashCode();
        }

        public bool Equals(SupplierPaymentView other)
        {
            return this.SupplierId.Equals(other.SupplierId);
        }
    }
}