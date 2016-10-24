using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Location;
using Model.Product;
using Model.Invoice;

namespace Model.Customer
{
    [Table("Customer")]
    public class CustomerEntity
    {
        [Key]
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

        // navigation properties
        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }

        //public virtual CustomerDueEntity CustomerDue { get; set; }

        //public virtual ICollection<ProductQuantityEntity> ProductTypeQuantities { get; set; }

        //public virtual ICollection<InvoiceInfoEntity> Invoices { get; set; }
    }

    [Table("CustomerDue")]
    public class CustomerDueEntity
    {
        [Key]
        [ForeignKey("Customer")]
        public string CustomerDueId { get; set; }

        [Required]
        [Display(Name = "Receive Amount")]
        public double ReceiveAmount { get; set; }

        // navigation properties
        public virtual CustomerEntity Customer { get; set; }
    }
}
