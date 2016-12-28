using Data.Helper;
using System.Linq;
using Model.Inventory;
using Model.Requisition;
using Data.Infrastructure;

namespace Data.Repositories.Requisition
{
    public class RequisitionRepository : RepositoryBase<RequisitionEntity>, IRequisitionRepository
    {
        public RequisitionRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override RequisitionEntity GetById(string requisitionId)
        {
            return Context.Requisition.Include("TenantInfo").FirstOrDefault(rqn => rqn.RequisitionId == requisitionId);
        }

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

                var tenantEntity = Context.Tenant.Find(requisitionEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
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
