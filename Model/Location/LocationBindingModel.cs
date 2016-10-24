using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.CompanyInfo;
using Model.Customer;
using Model.Supplier;

namespace Model.Location
{

    [Table("Location")]
    public class LocationEntity
    {
        [Key]
        public string LocationId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        // navigation properties
        //public virtual ICollection<CompanyInfoEntity> Companies { get; set; }

        //public virtual ICollection<CustomerEntity> Customers { get; set; }

        //public virtual ICollection<SupplierEntity> Suppliers { get; set; }
    }
}
