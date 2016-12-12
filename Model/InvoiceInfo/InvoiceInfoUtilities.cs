using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Sale;
using Model.Purchase;

namespace Model.InvoiceInfo
{
    [Table("InvoiceInfo")]
    public class InvoiceInfoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string InvoiceInfoId { get; set; }

        public string BillEntryId { get; set; }

        public string PurchaseEntryId { get; set; }

        public string SaleReturnId { get; set; }

        public string PurchaseReturnId { get; set; }

        [Required]
        [Display(Name = "Invoice Status")]
        public bool Status { get; set; }

        // navigation properties
        [ForeignKey("BillEntryId")]
        public virtual BillEntryEntity BillEntry { get; set; }

        [ForeignKey("PurchaseEntryId")]
        public virtual PurchaseEntryEntity PurchaseEntry { get; set; }

        [ForeignKey("SaleReturnId")]
        public virtual SaleReturnEntity SaleReturn { get; set; }

        [ForeignKey("PurchaseReturnId")]
        public virtual PurchaseReturnEntity PurchaseReturn { get; set; }
    }
}
