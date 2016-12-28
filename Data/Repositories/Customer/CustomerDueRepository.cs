using Data.Helper;
using System.Linq;
using Model.Customer;
using Data.Infrastructure;

namespace Data.Repositories.Customer
{
    public class CustomerDueRepository : RepositoryBase<CustomerDueEntity>, ICustomerDueRepository
    {
        public CustomerDueRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override CustomerDueEntity GetById(string customerDueId)
        {
            return Context.CustomerDue.Include("Customer").Include("TenantInfo").FirstOrDefault(cusdue => cusdue.CustomerDueId == customerDueId);
        }

        public override bool Delete(string customerDueId)
        {
            var customerDueEntity = Context.CustomerDue.Find(customerDueId);
            if (customerDueEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(customerDueEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.CustomerDue.Remove(customerDueEntity);
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
