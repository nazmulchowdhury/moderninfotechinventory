using Data.Infrastructure;
using Data.Helper;
using Model.Requisition;
using Model.Inventory;
using System.Linq;

namespace Data.Repositories.Requisition
{
    public class RequisitionRepository : RepositoryBase<RequisitionEntity>, IRequisitionRepository
    {
        public RequisitionRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override bool Delete(string requisitionId)
        {
            var requisitionEntity = Context.Requisition.Find(requisitionId);

            if (requisitionEntity != null)
            {
                Context.Entry(requisitionEntity).Collection("ProductQuantities").Load();
                var productQuantities = requisitionEntity.ProductQuantities.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    requisitionEntity.ProductQuantities.Remove(productQuantity);
                    Context.ProductQuantity.Remove(productQuantity);
                }

                Context.Requisition.Remove(requisitionEntity);
                Context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
