using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Product;
using System;

namespace Model.Inventory
{
    [Table("Inventory")]
    public class InventoryEntity
    {
        [Key]
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

        // navigation properties
        [ForeignKey("ProductQuantityId")]
        public virtual ProductQuantityEntity ProductQuantity { get; set; }
    }
}
