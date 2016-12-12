using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Supplier;
using Model.Inventory;
using System;
using System.Collections.Generic;

namespace Model.Purchase
{
    [Table("PurchaseEntry")]
    public class PurchaseEntryEntity : IEquatable<PurchaseEntryEntity>
    {
        public PurchaseEntryEntity()
        {
            this.ProductQuantities = new HashSet<ProductQuantityEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PurchaseEntryId { get; set; }

        [Required]
        public string SupplierId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receive Date")]
        public DateTime ReceiveDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Receive Number")]
        public string ReceiveNumber { get; set; }

        [Display(Name = "Paid Amount")]
        public double PaidAmount { get; set; }

        // navigation properties
        [ForeignKey("SupplierId")]
        public virtual SupplierEntity Supplier { get; set; }

        public virtual ICollection<ProductQuantityEntity> ProductQuantities { get; set; }

        public override int GetHashCode()
        {
            return PurchaseEntryId.GetHashCode();
        }

        public bool Equals(PurchaseEntryEntity other)
        {
            return this.PurchaseEntryId.Equals(other.PurchaseEntryId);
        }
    }

    [Table("PurchaseReturn")]
    public class PurchaseReturnEntity : IEquatable<PurchaseReturnEntity>
    {
        public PurchaseReturnEntity()
        {
            this.ProductReturnQuantities = new HashSet<ProductReturnQuantityEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PurchaseReturnId { get; set; }

        [Required]
        [Display(Name = "Reference Invoice ID")]
        public string RefInvoiceId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Penalty")]
        public double Penalty { get; set; }

        [Required]
        [Display(Name = "Paid Amount")]
        public double PaidAmount { get; set; }

        // navigation properties
        public virtual ICollection<ProductReturnQuantityEntity> ProductReturnQuantities { get; set; }

        public override int GetHashCode()
        {
            return PurchaseReturnId.GetHashCode();
        }

        public bool Equals(PurchaseReturnEntity other)
        {
            return this.PurchaseReturnId.Equals(other.PurchaseReturnId);
        }
    }
}
