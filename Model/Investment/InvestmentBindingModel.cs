using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Investment
{
    [Table("Investment")]
    public class InvestmentEntity
    {
        [Key]
        public string InvestmentId { get; set; }

        [Required]
        [Display(Name = "Investment Amount")]
        public double Amount { get; set; }
    }
}
