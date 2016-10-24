using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.CompanyInfo;

namespace Model.Logo
{

    [Table("Logo")]
    public class LogoEntity
    {
        [Key]
        public string LogoId { get; set; }

        // navigation properties
        //public virtual ICollection<CompanyInfoEntity> Companies { get; set; }
    }
}
