﻿using Model.Tenant;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Location
{

    [Table("Location")]
    public class LocationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string LocationId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }
    }
}
