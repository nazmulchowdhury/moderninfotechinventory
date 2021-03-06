﻿using System;
using Model.Sale;
using Model.Tenant;
using Model.Purchase;
using Model.Requisition;
using Model.DeliveryOrder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Inventory
{
    [Table("Category")]
    public class CategoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CategoryId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }
    }

    [Table("Unit")]
    public class UnitEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UnitId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }
    }

    [Table("SubCategory")]
    public class SubCategoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SubCategoryId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SubCategory Name")]
        public string SubCategoryName { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string UnitId { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CategoryEntity Category { get; set; }
        [ForeignKey("UnitId")]
        public virtual UnitEntity Unit { get; set; }
    }

    [Table("Product")]
    public class ProductInfoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProductId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Barcode")]
        public string Barcode { get; set; }

        [Required]
        [Display(Name = "Cost Price")]
        public double CostPrice { get; set; }

        [Required]
        [Display(Name = "Sale Price")]
        public double SalePrice { get; set; }

        [Required]
        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }

        [Required]
        public string SubCategoryId { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategoryEntity SubCategory { get; set; }
    }

    [Table("ProductQuantity")]
    public class ProductQuantityEntity : IEquatable<ProductQuantityEntity>
    {
        public ProductQuantityEntity()
        {
            this.SaleEntries = new HashSet<SaleEntryEntity>();
            this.PurchaseEntries = new HashSet<PurchaseEntryEntity>();
            this.Stocks = new HashSet<StockAdjustmentEntity>();
            this.Requisitions = new HashSet<RequisitionEntity>();
            this.DeliveryOrders = new HashSet<DeliveryOrderEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProductQuantityId { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public double? Price { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductInfoEntity Product { get; set; }

        public virtual ICollection<SaleEntryEntity> SaleEntries { get; set; }

        public virtual ICollection<PurchaseEntryEntity> PurchaseEntries { get; set; }

        public virtual ICollection<StockAdjustmentEntity> Stocks { get; set; }

        public virtual ICollection<RequisitionEntity> Requisitions { get; set; }

        public virtual ICollection<DeliveryOrderEntity> DeliveryOrders { get; set; }

        public override int GetHashCode()
        {
            return ProductId.GetHashCode();
        }

        public bool Equals(ProductQuantityEntity other)
        {
            return this.ProductId.Equals(other.ProductId);
        }
    }

    [Table("ProductReturnQuantity")]
    public class ProductReturnQuantityEntity : IEquatable<ProductReturnQuantityEntity>
    {
        public ProductReturnQuantityEntity()
        {
            this.SaleReturns = new HashSet<SaleReturnEntity>();
            this.PurchaseReturns = new HashSet<PurchaseReturnEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProductReturnQuantityId { get; set; }

        [Required]
        [Display(Name = "Return Quantity")]
        public int ReturnQuantity { get; set; }

        [Required]
        public string ProductQuantityId { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("ProductQuantityId")]
        public virtual ProductQuantityEntity ProductQuantity { get; set; }

        public virtual ICollection<SaleReturnEntity> SaleReturns { get; set; }

        public virtual ICollection<PurchaseReturnEntity> PurchaseReturns { get; set; }

        public override int GetHashCode()
        {
            return ProductQuantityId.GetHashCode();
        }

        public bool Equals(ProductReturnQuantityEntity other)
        {
            return this.ProductQuantityId.Equals(other.ProductQuantityId);
        }
    }

    [Table("DamageStockEntry")]
    public class DamageStockEntryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string DamageStockEntryId { get; set; }

        [Required]
        public string ProductQuantityId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("ProductQuantityId")]
        public virtual ProductQuantityEntity ProductQuantity { get; set; }
    }

    [Table("StockAdjustment")]
    public class StockAdjustmentEntity : IEquatable<StockAdjustmentEntity>
    {
        public StockAdjustmentEntity()
        {
            this.ProductQuantities = new HashSet<ProductQuantityEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StockAdjustmentId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receive Date")]
        public DateTime ReceiveDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Receive Number")]
        public string ReceiveNumber { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        public virtual ICollection<ProductQuantityEntity> ProductQuantities { get; set; }

        public override int GetHashCode()
        {
            return StockAdjustmentId.GetHashCode();
        }

        public bool Equals(StockAdjustmentEntity other)
        {
            return this.StockAdjustmentId.Equals(other.StockAdjustmentId);
        }
    }

    [Table("Inventory")]
    public class InventoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string InventoryId { get; set; }

        [Required]
        public string ProductQuantityId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receive Date")]
        public DateTime ReceiveDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Receive Number")]
        public string ReceiveNumber { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("ProductQuantityId")]
        public virtual ProductQuantityEntity ProductQuantity { get; set; }
    }
}
