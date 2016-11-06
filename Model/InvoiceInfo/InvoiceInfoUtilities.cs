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
        public string InvoiceInfoId { get; set; }

        public string BillEntryId { get; set; }

        public string PurchaseEntryId { get; set; }

        // navigation properties
        [ForeignKey("BillEntryId")]
        public virtual BillEntryEntity BillEntry { get; set; }

        [ForeignKey("PurchaseEntryId")]
        public virtual PurchaseEntryEntity PurchaseEntry { get; set; }
    }
}
