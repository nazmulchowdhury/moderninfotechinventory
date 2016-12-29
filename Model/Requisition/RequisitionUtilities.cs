using System;
using Model.Inventory;
using Model.Tenant;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Requisition
{
    [Table("Requisition")]
    public class RequisitionEntity : IEquatable<RequisitionEntity>
    {
        public RequisitionEntity()
        {
            this.ProductQuantities = new HashSet<ProductQuantityEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RequisitionId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Requisition Date")]
        public DateTime RequisitionDate { get; set; }

        [Display(Name = "Is Approved")]
        public bool IsApproved { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Requisition Description")]
        public string Description { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        public virtual ICollection<ProductQuantityEntity> ProductQuantities { get; set; }

        public override int GetHashCode()
        {
            return RequisitionId.GetHashCode();
        }

        public bool Equals(RequisitionEntity other)
        {
            return this.RequisitionId.Equals(other.RequisitionId);
        }
    }
}
