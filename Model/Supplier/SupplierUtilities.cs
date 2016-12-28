using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Location;
using Model.BaseModel;

namespace Model.Supplier
{
    [Table("Supplier")]
    public class SupplierEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SupplierId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }

        [Required]
        public string LocationId { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }
    }

    [Table("SupplierPayment")]
    public class SupplierPaymentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SupplierPaymentId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Paid Amount")]
        public double PaidAmount { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Supplier Description")]
        public string Description { get; set; }

        [Required]
        public string SupplierId { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("SupplierId")]
        public virtual SupplierEntity Supplier { get; set; }
    }
}
