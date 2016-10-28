using Data.Infrastructure;
using Data.Helper;
using Model.Customer;
using System.Linq;

namespace Data.Repositories.Customer
{
    public class CustomerRepository : RepositoryBase<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override CustomerEntity GetById(string customerId)
        {
            return DbContext.Customer.Include("Location").FirstOrDefault(cus => cus.CustomerId == customerId);
        }
    }
}
