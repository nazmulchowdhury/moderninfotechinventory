using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.CompanyInfo;
using System;

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

    [Table("Cash")]
    public class CashEntity
    {
        [Key]
        public string CashId { get; set; }

        [Required]
        [Display(Name = "Cash Amount")]
        public double Amount { get; set; }
    }

    [Table("Expense")]
    public class ExpenseEntity
    {
        [Key]
        public string ExpenseId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expense Date")]
        public DateTime ExpenseDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Purpose of Expense")]
        public string Purpose { get; set; }

        [Required]
        [Display(Name = "Expense Amount")]
        public double Amount { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Expensed By")]
        public string ExpensedBy { get; set; }
    }

    [Table("Vat")]
    public class VatEntity
    {
        [Key]
        public string VatId { get; set; }

        [Required]
        [Display(Name = "Vat Amount")]
        public double VatAmount { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Vat Area")]
        public string VatArea { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Vat Registration Number")]
        public string VatRegistrationNumber { get; set; }
    }
}
