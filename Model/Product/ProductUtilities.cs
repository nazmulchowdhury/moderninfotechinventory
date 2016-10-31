using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Customer;

namespace Model.Product
{
    [Table("Category")]
    public class CategoryEntity
    {
        [Key]
        public string CategoryId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        // navigation properties
        //public virtual ICollection<SubCategoryEntity> SubCategories { get; set; }
    }

    [Table("SubCategory")]
    public class SubCategoryEntity
    {
        [Key]
        public string SubCategoryId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SubCategory Name")]
        public string SubCategoryName { get; set; }

        [Required]
        public string CategoryId { get; set; }

        // navigation properties
        [ForeignKey("CategoryId")]
        public virtual CategoryEntity Category { get; set; }

        //public virtual ICollection<ProductEntity> Products { get; set; }
    }

    [Table("ProductInfo")]
    public class ProductInfoEntity
    {
        [Key]
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

        // navigation properties
        [ForeignKey("SubCategoryId")]
        public virtual SubCategoryEntity SubCategory { get; set; }

        //public virtual ProductQuantityEntity ProductQuantity { get; set; }
    }

    [Table("ProductQuantity")]
    public class ProductQuantityEntity
    {
        [Key]
        [ForeignKey("Product")]
        public string ProductQuantityId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        public string CustomerId { get; set; }

        // navigation properties
        public virtual ProductInfoEntity Product { get; set; }

        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }

        //public virtual ICollection<StockAdjustmentEntity> Stocks { get; set; }

        //public virtual SaleReturnQuantityEntity SaleProductReturnQuantity { get; set; }
    }

    [Table("StockAdjustment")]
    public class StockAdjustmentEntity
    {
        [Key]
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
        public string ProductQuantityId { get; set; }

        // navigation properties
        [ForeignKey("ProductQuantityId")]
        public virtual ProductQuantityEntity ProductQuantity { get; set; }

        //public virtual DamageStockEntryEntity DamageStockQuantity { get; set; }
    }

    [Table("DamageStockEntry")]
    public class DamageStockEntryEntity
    {
        [Key]
        [ForeignKey("StockAdjustment")]
        public string DamageStockEntryId { get; set; }

        [Required]
        [Display(Name = "Damage Quantity")]
        public int DamageQuantity { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        // navigation properties
        public virtual StockAdjustmentEntity StockAdjustment { get; set; }
    }

    [Table("SaleReturnQuantity")]
    public class SaleReturnQuantityEntity
    {
        [Key]
        [ForeignKey("ProductQuantity")]
        public string SaleReturnQuantityId { get; set; }

        [Required]
        [Display(Name = "Return Quantity")]
        public int ReturnQuantity { get; set; }

        // navigation properties
        public virtual ProductQuantityEntity ProductQuantity { get; set; }
    }
}
