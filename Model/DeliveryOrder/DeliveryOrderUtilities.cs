using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Model.Inventory;
using Model.Requisition;
using Model.BaseModel;
using System.Collections.Generic;
using System;

namespace Model.DeliveryOrder
{
    [Table("DeliveryOrder")]
    public class DeliveryOrderEntity : IEquatable<DeliveryOrderEntity>
    {
        public DeliveryOrderEntity()
        {
            this.ProductQuantities = new HashSet<ProductQuantityEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string DeliveryOrderId { get; set; }

        [Required]
        public string RequisitionId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery Order Date")]
        public DateTime DeliveryOrderDate { get; set; }

        [Display(Name = "Is Received")]
        public bool IsReceived { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Delivery Order Description")]
        public string Description { get; set; }

        [Required]
        public string TenantId { get; set; }

        // navigation properties
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantInfo { get; set; }

        [ForeignKey("RequisitionId")]
        public virtual RequisitionEntity Requisition { get; set; }

        public virtual ICollection<ProductQuantityEntity> ProductQuantities { get; set; }

        public override int GetHashCode()
        {
            return DeliveryOrderId.GetHashCode();
        }

        public bool Equals(DeliveryOrderEntity other)
        {
            return this.DeliveryOrderId.Equals(other.DeliveryOrderId);
        }
    }
}
