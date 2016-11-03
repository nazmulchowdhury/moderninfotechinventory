using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Supplier;
using Model.Product;
using Model.Invoice;
using System;

namespace Model.Purchase
{
    [Table("PurchaseEntry")]
    public class PurchaseEntryEntity
    {
        [Key]
        public string PurchaseEntryId { get; set; }

        [Required]
        public string SupplierId { get; set; }

        [Required]
        public string ProductQuantityId { get; set; }

        [Required]
        public string InvoiceInfoId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receive Date")]
        public DateTime ReceiveDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Receive Number")]
        public string ReceiveNumber { get; set; }

        // navigation properties
        [ForeignKey("SupplierId")]
        public virtual SupplierEntity Supplier { get; set; }

        [ForeignKey("ProductQuantityId")]
        public virtual ProductQuantityEntity ProductQuantity { get; set; }

        [ForeignKey("InvoiceInfoId")]
        public virtual InvoiceInfoEntity InvoiceInfo { get; set; }
    }

    [Table("PurchaseReturn")]
    public class PurchaseReturnEntity
    {
        [Key]
        [ForeignKey("PurchaseEntry")]
        public string PurchaseReturnId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Return Number")]
        public string ReturnNumber { get; set; }

        // navigation properties
        public virtual PurchaseEntryEntity PurchaseEntry { get; set; }
    }
}
