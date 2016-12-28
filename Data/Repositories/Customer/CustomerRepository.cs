using Data.Helper;
using System.Linq;
using Model.Customer;
using Data.Infrastructure;

namespace Data.Repositories.Customer
{
    public class CustomerRepository : RepositoryBase<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override CustomerEntity GetById(string customerId)
        {
            return Context.Customer.Include("Location").Include("TenantInfo").FirstOrDefault(cus => cus.CustomerId == customerId);
        }

        public override bool Delete(string customerId)
        {
            var customerEntity = Context.Customer.Find(customerId);
            if (customerEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(customerEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Customer.Remove(customerEntity);
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
