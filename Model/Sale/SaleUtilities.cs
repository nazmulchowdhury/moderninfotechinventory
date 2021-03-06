﻿using System;
using Model.Tenant;
using Model.Customer;
using Model.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Sale
{
    [Table("SaleEntry")]
    public class SaleEntryEntity : IEquatable<SaleEntryEntity>
    {
        public SaleEntryEntity()
        {
            this.SaledProducts = new HashSet<ProductQuantityEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SaleEntryId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Display(Name = "Discount")]
        public double Discount { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }

        public virtual ICollection<ProductQuantityEntity> SaledProducts { get; set; }

        public override int GetHashCode()
        {
            return SaleEntryId.GetHashCode();
        }

        public bool Equals(SaleEntryEntity other)
        {
            return this.SaleEntryId.Equals(other.SaleEntryId);
        }
    }

    [Table("SaleReturn")]
    public class SaleReturnEntity : IEquatable<SaleReturnEntity>
    {
        public SaleReturnEntity()
        {
            this.SaleReturnedProducts = new HashSet<ProductReturnQuantityEntity>();
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

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        public virtual ICollection<ProductReturnQuantityEntity> SaleReturnedProducts { get; set; }

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
