using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Location;
using System;

namespace Model.Investor
{
    [Table("Investor")]
    public class InvestorEntity
    {
        [Key]
        public string InvestorId { get; set; }

        [Required]
        public string LocationId { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Investor Phone Number")]
        public string PhoneNumber {get; set; }

        [Required]
        [Display(Name = "Investor Balance")]
        public double Balance { get; set; }

        // navigation properties
        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }
    }

    [Table("InvestorTransaction")]
    public class InvestorTransactionEntity
    {
        [Key]
        [ForeignKey("Investor")]
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

        // navigation properties
        public virtual InvestorEntity Investor { get; set; }
    }
}
