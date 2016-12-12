using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Location;

namespace Model.Customer
{
    [Table("Customer")]
    public class CustomerEntity
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

        // navigation properties
        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }
    }

    [Table("CustomerDue")]
    public class CustomerDueEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CustomerDueId { get; set; }

        [Required]
        [Display(Name = "Receive Amount")]
        public double ReceiveAmount { get; set; }

        [Required]
        public string CustomerId { get; set; }

        // navigation properties
        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }
    }
}
