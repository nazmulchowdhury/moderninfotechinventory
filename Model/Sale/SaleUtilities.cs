using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Inventory;
using Model.Customer;
using System.Collections.Generic;
using System;

namespace Model.Sale
{
    [Table("BillEntry")]
    public class BillEntryEntity : IEquatable<BillEntryEntity>
    {
        public BillEntryEntity()
        {
            this.ProductQuantities = new HashSet<ProductQuantityEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string BillEntryId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Display(Name = "Discount")]
        public double Discount { get; set; }

        // navigation properties
        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }

        public virtual ICollection<ProductQuantityEntity> ProductQuantities { get; set; }

        public override int GetHashCode()
        {
            return BillEntryId.GetHashCode();
        }

        public bool Equals(BillEntryEntity other)
        {
            return this.BillEntryId.Equals(other.BillEntryId);
        }
    }

    [Table("SaleReturn")]
    public class SaleReturnEntity : IEquatable<SaleReturnEntity>
    {
        public SaleReturnEntity()
        {
            this.ProductReturnQuantities = new HashSet<ProductReturnQuantityEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SaleReturnId { get; set; }

        [Required]
        [Display(Name = "Reference Invoice ID")]
        public string RefInvoiceId { get; set; }

        [Display(Name = "Penalty")]
        public double Penalty { get; set; }

        [Required]
        [Display(Name = "Paid Amount")]
        public double PaidAmount { get; set; }

        // navigation properties
        public virtual ICollection<ProductReturnQuantityEntity> ProductReturnQuantities { get; set; }

        public override int GetHashCode()
        {
            return SaleReturnId.GetHashCode();
        }

        public bool Equals(SaleReturnEntity other)
        {
            return this.SaleReturnId.Equals(other.SaleReturnId);
        }
    }
}
