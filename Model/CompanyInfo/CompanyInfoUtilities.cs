using Model.User;
using Model.Location;
using Model.Tenant;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.CompanyInfo
{
    [Table("CompanyInfo")]
    public class CompanyInfoEntity
    {
        [Key]
        [ForeignKey("LoggedUser")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string LocationId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Company Description")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Note")]
        public string Note { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }

        public virtual UserEntity LoggedUser { get; set; }
    }
}
