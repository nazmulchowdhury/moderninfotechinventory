using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.CompanyInfo;

namespace Model.Client
{
    [Table("Clients")]
    public class ClientEntity
    {
        [Key]
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public string CompanyId { get; set; }

        // navigation properties
        [ForeignKey("CompanyId")]
        public virtual CompanyInfoEntity Company { get; set; }
    }
}
