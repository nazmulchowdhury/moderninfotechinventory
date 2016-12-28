using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Model.Utilities;
using Model.BaseModel;

namespace Model.InvoiceInfo
{
    [Table("InvoiceInfo")]
    public class InvoiceInfoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string InvoiceInfoId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Entry Id")]
        public string EntryId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Entry Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Option EntryType { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }
    }
}
