using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Utilities
{
    public enum Option
    {
        SALE_ENTRY = 1, SALE_RETURN, PURCHASE_ENTRY, PURCHASE_RETURN, DAMAGE_ENTRY
    }

    public class CurrentStock
    {
        public string ProductName { get; set; }
        public int PurchaseQuantity { get; set; }
        public int PurchaseReturnQuantity { get; set; }
        public int SaleQuantity { get; set; }
        public int SaleReturnQuantity { get; set; }
        public int DamageQuantity { get; set; }
        public int CurrentStockQuantity { get; set; }
    }

    public partial class StockedProductDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProductQuantityId { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
    }

    public class StockedProductQuantity : StockedProductDetails
    {
        [Required]
        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }
    }

    public class StockedProductReturnQuantity : StockedProductDetails
    {
        [Required]
        [Display(Name = "Product Return Quantity")]
        public int ReturnQuantity { get; set; }
    }
}
