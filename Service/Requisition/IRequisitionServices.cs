using System.Collections.Generic;
using Model.Requisition;

namespace Service.Requisition
{
    public interface IRequisitionServices
    {
        ICollection<RequisitionEntity> GetAllRequisitions();
        RequisitionEntity GetRequisition(string requisitionId);
        RequisitionEntity CreateRequisition(RequisitionEntity requisitionEntity);
        bool UpdateRequisition(string requisitionId, RequisitionEntity requisitionEntity);
        bool DeleteRequisition(string requisitionId);
    }
}
