using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Location;
using Model.Logo;
using Model.Client;
using Model.Investment;

namespace Model.CompanyInfo
{

    [Table("CompanyInfo")]
    public class CompanyInfoEntity
    {
        [Key]
        public string CompanyId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Company Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string LocationId { get; set; }

        [Required]
        public string LogoId { get; set; }

        [Required]
        public string UserId { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Note")]
        public string Note { get; set; }

        [Display(Name = "Company Status")]
        public bool Status { get; set; }

        // navigation properties
        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }

        [ForeignKey("LogoId")]
        public virtual LogoEntity Logo { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        //public virtual ICollection<ClientEntity> Clients { get; set; }
    }

    [Table("AspNetUsers")]
    public class User 
    {
        [Key]
        public string Id { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string UserName { get; set; }

        // navigation properties
        //public virtual ICollection<CompanyInfoEntity> Companies { get; set; }
        //public virtual InvestmentEntity Investment { get; set; }
    }
}
