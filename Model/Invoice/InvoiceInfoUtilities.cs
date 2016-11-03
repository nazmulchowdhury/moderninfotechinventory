using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Invoice
{
    [Table("InvoiceInfo")]
    public class InvoiceInfoEntity
    {
        [Key]
        public string InvoiceInfoId { get; set; }
    }
}
