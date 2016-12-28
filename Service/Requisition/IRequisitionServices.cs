using Model.Requisition;
using System.Collections.Generic;

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
