using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Location;
using System;

namespace Model.Vat
{
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

        // navigation properties
        [ForeignKey("LocationId")]
        public virtual LocationEntity Location { get; set; }
    }
}
