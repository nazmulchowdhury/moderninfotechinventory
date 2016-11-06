using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Product;
using Model.Customer;
using Model.InvoiceInfo;

namespace Model.Sale
{
    [Table("BillEntry")]
    public class BillEntryEntity
    {
        [Key]
        public string BillEntryId { get; set; }

        [Required]
        public string ProductQuantityId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        // navigation properties
        [ForeignKey("ProductQuantityId")]
        public virtual ProductQuantityEntity ProductQuantity { get; set; }

        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }
    }

    [Table("SaleReturnQuantity")]
    public class SaleReturnQuantityEntity
    {
        [Key]
        [ForeignKey("BillEntry")]
        public string SaleReturnQuantityId { get; set; }

        [Required]
        [Display(Name = "Return Quantity")]
        public int ReturnQuantity { get; set; }

        // navigation properties
        public virtual BillEntryEntity BillEntry { get; set; }
    }
}
