using System.Collections.Generic;
using Data.Repositories.Requisition;
using Model.Requisition;

namespace Service.Requisition
{
    public class RequisitionServices : IRequisitionServices
    {
        private readonly IRequisitionRepository requisitionRepository;

        public RequisitionServices(IRequisitionRepository requisitionRepository)
        {
            this.requisitionRepository = requisitionRepository;
        }

        public ICollection<RequisitionEntity> GetAllRequisitions()
        {
            return requisitionRepository.GetAll();
        }

        public RequisitionEntity GetRequisition(string requisitionId)
        {
            return requisitionRepository.GetById(requisitionId);
        }

        public RequisitionEntity CreateRequisition(RequisitionEntity requisitionEntity)
        {
            return requisitionRepository.Add(requisitionEntity);
        }

        public bool UpdateRequisition(string requisitionId, RequisitionEntity requisitionEntity)
        {
            var storedItem = requisitionRepository.GetById(requisitionId);

            if (storedItem != null)
            {
                storedItem.RequisitionDate = requisitionEntity.RequisitionDate;
                storedItem.IsApproved = requisitionEntity.IsApproved;
                storedItem.Description = requisitionEntity.Description;

                requisitionRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteRequisition(string requisitionId)
        {
            return requisitionRepository.Delete(requisitionId);
        }
    }
}
