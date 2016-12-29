using System;
using Model.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Tenant
{
    [Table("Tenant")]
    [Serializable]
    public class TenantEntity
    {
        public TenantEntity()
        { }

        public TenantEntity(string userId)
        {
            this.TenantId = Guid.NewGuid().ToString();
            this.UserId = userId;
            this.ActivationDate = DateTime.Now;
            this.Status = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TenantId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Activation Date")]
        public DateTime ActivationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Inactivation Date")]
        public DateTime? InactivationDate { get; set; }

        [Required]
        public bool Status { get; set; }

        // navigation properties
        [ForeignKey("UserId")]
        public virtual UserEntity LoggedUser { get; set; }
    }
}
