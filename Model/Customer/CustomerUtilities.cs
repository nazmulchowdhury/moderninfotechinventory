using System;
using Model.Location;
using Model.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Customer
{
    [Table("Customer")]
    public class CustomerEntity : IEquatable<CustomerEntity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CustomerId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string LocationId { get; set; }

        [Required]
        [Display(Name = "Current Due")]
        public double CurrentDue { get; set; }

        [Required]
        [Display(Name = "Due Limit")]
        public double DueLimit { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }

        public override int GetHashCode()
        {
            return CustomerId.GetHashCode();
        }

        public bool Equals(CustomerEntity other)
        {
            return this.CustomerId.Equals(other.CustomerId);
        }
    }

    [Table("CustomerDue")]
    public class CustomerDueEntity : IEquatable<CustomerDueEntity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CustomerDueId { get; set; }

        [Required]
        [Display(Name = "Receive Amount")]
        public double ReceiveAmount { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }

        public override int GetHashCode()
        {
            return CustomerDueId.GetHashCode();
        }

        public bool Equals(CustomerDueEntity other)
        {
            return this.CustomerDueId.Equals(other.CustomerDueId);
        }
    }
}
