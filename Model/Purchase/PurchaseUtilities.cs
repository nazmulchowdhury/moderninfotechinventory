using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Supplier;
using Model.Inventory;
using Model.Tenant;
using System;
using System.Collections.Generic;

namespace Model.Purchase
{
    [Table("PurchaseEntry")]
    public class PurchaseEntryEntity : IEquatable<PurchaseEntryEntity>
    {
        public PurchaseEntryEntity()
        {
            this.PurchasedProducts = new HashSet<ProductQuantityEntity>();
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

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("SupplierId")]
        public virtual SupplierEntity Supplier { get; set; }

        public virtual ICollection<ProductQuantityEntity> PurchasedProducts { get; set; }

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
            this.PurchaseReturnedProducts = new HashSet<ProductReturnQuantityEntity>();
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

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        public virtual ICollection<ProductReturnQuantityEntity> PurchaseReturnedProducts { get; set; }

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
