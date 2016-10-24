using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Customer;

namespace Model.Invoice
{
    [Table("InvoiceInfo")]
    public class InvoiceInfoEntity
    {
        [Key]
        public string InvoiceInfoId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        // navigation properties
        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }
    }
}
