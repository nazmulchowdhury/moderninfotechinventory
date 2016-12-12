using System;

namespace ModernInfoTechInventory.ViewModels.Customer
{
    public class CustomerView : IEquatable<CustomerView>
    {
        public string CustomerName { get; set; }
        
        public string PhoneNumber { get; set; }

        public string LocationId { get; set; }

        public double CurrentDue { get; set; }

        public double DueLimit { get; set; }

        public override int GetHashCode()
        {
            return PhoneNumber.GetHashCode();
        }

        public bool Equals(CustomerView other)
        {
            return this.PhoneNumber.Equals(other.PhoneNumber);
        }
    }
    
    public class CustomerDueView : IEquatable<CustomerDueView>
    {
        public double ReceiveAmount { get; set; }
        
        public string CustomerId { get; set; }

        public override int GetHashCode()
        {
            return CustomerId.GetHashCode();
        }

        public bool Equals(CustomerDueView other)
        {
            return this.CustomerId.Equals(other.CustomerId);
        }
    }
}