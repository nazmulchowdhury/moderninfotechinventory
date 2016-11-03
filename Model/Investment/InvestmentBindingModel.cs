using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.CompanyInfo;

namespace Model.Investment
{
    [Table("Investment")]
    public class InvestmentEntity
    {
        [Key]
        [ForeignKey("Company")]
        public string InvestmentId { get; set; }

        [Required]
        [Display(Name = "Investment Amount")]
        public double Amount { get; set; }

        // navigation properties
        public virtual CompanyInfoEntity Company { get; set; }
    }
}
