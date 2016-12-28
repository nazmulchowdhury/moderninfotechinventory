using System;
using Model.Location;
using Model.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Accounts
{
    [Table("Investment")]
    public class InvestmentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string InvestmentId { get; set; }

        [Required]
        [Display(Name = "Investment Amount")]
        public double Amount { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }
    }

    [Table("Cash")]
    public class CashEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CashId { get; set; }

        [Required]
        [Display(Name = "Cash Amount")]
        public double Amount { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }
    }

    [Table("Vat")]
    public class VatEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string VatId { get; set; }

        [Required]
        [Display(Name = "Vat Amount")]
        public double VatAmount { get; set; }

        [Required]
        public string LocationId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Vat Registration Number")]
        public string VatRegistrationNumber { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }
    }

    [Table("Expense")]
    public class ExpenseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }
    }

    [Table("Investor")]
    public class InvestorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string InvestorId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Investor Name")]
        public string InvestorName { get; set; }

        [Required]
        public string LocationId { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Investor Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Investor Balance")]
        public double Balance { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }
    }

    [Table("InvestorTransaction")]
    public class InvestorTransactionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string InvestorTransactionId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Display(Name = "Transaction Amount")]
        public double Amount { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Transaction Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public bool TransactionType { get; set; }

        [Required]
        public string InvestorId { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("InvestorId")]
        public virtual InvestorEntity Investor { get; set; }
    }
}
