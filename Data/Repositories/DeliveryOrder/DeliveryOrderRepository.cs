using Data.Infrastructure;
using Data.Helper;
using Model.DeliveryOrder;
using Model.Inventory;
using System.Linq;

namespace Data.Repositories.DeliveryOrder
{
    public class DeliveryOrderRepository : RepositoryBase<DeliveryOrderEntity>, IDeliveryOrderRepository
    {
        public DeliveryOrderRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override DeliveryOrderEntity GetById(string deliveryOrderId)
        {
            return Context.DeliveryOrder.Include("Requisition").FirstOrDefault(deliveryOrder => deliveryOrder.DeliveryOrderId == deliveryOrderId);
        }

        public override bool Delete(string deliveryOrderId)
        {
            var deliveryOrderEntity = Context.DeliveryOrder.Find(deliveryOrderId);

            if (deliveryOrderEntity != null)
            {
                Context.Entry(deliveryOrderEntity).Collection("ProductQuantities").Load();
                var productQuantities = deliveryOrderEntity.ProductQuantities.ToList();

                foreach (ProductQuantityEntity productQuantity in productQuantities)
                {
                    deliveryOrderEntity.ProductQuantities.Remove(productQuantity);
                    Context.ProductQuantity.Remove(productQuantity);
                }

                Context.DeliveryOrder.Remove(deliveryOrderEntity);
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
