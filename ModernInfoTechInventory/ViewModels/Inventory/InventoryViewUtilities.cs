using System;
using System.Collections.Generic;

namespace ModernInfoTechInventory.ViewModels.Inventory
{
    public class CategoryView
    {
        public string CategoryName { get; set; }
    }

    public class SubCategoryView
    {
        public string SubCategoryName { get; set; }

        public string CategoryId { get; set; }
    }

    public class ProductInfoView
    {
        public string ProductName { get; set; }

        public string Barcode { get; set; }

        public double CostPrice { get; set; }

        public double SalePrice { get; set; }

        public int ReorderLevel { get; set; }

        public string SubCategoryId { get; set; }
    }

    public class ProductQuantityView : IEquatable<ProductQuantityView>
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public override int GetHashCode()
        {
            return ProductId.GetHashCode();
        }

        public bool Equals(ProductQuantityView other)
        {
            return this.ProductId.Equals(other.ProductId);
        }
    }

    public class ProductReturnQuantityView : IEquatable<ProductReturnQuantityView>
    {
        public int ReturnQuantity { get; set; }

        public string ProductQuantityId { get; set; }

        public override int GetHashCode()
        {
            return ProductQuantityId.GetHashCode();
        }

        public bool Equals(ProductReturnQuantityView other)
        {
            return this.ProductQuantityId.Equals(other.ProductQuantityId);
        }
    }

    public class DamageStockEntryView : IEquatable<DamageStockEntryView>
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public string Remark { get; set; }

        public override int GetHashCode()
        {
            return ProductId.GetHashCode();
        }

        public bool Equals(DamageStockEntryView other)
        {
            return this.ProductId.Equals(other.ProductId);
        }
    }

    public class StockAdjustmentView
    {
        public StockAdjustmentView()
        {
            this.ProductQuantities = new HashSet<ProductQuantityView>();
        }

        public DateTime ReceiveDate { get; set; }

        public string ReceiveNumber { get; set; }

        public ICollection<ProductQuantityView> ProductQuantities { get; set; }
    }
}