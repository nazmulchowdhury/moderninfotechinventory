using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    }

    [Table("Product")]
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

        // navigation properties
        public virtual ProductInfoEntity Product { get; set; }
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
    }

    [Table("DamageStockEntry")]
    public class DamageStockEntryEntity
    {
        [Key]
        public string DamageStockEntryId { get; set; }

        [Required]
        public string ProductQuantityId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        // navigation properties
        [ForeignKey("ProductQuantityId")]
        public virtual ProductQuantityEntity ProductQuantity { get; set; }
    }
}
