using Data.Infrastructure;
using Data.Helper;
using Model.Customer;
using System.Linq;

namespace Data.Repositories.Customer
{
    public class CustomerDueRepository : RepositoryBase<CustomerDueEntity>, ICustomerDueRepository
    {
        public CustomerDueRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override CustomerDueEntity GetById(string customerDueId)
        {
            return Context.CustomerDue.Include("Customer").FirstOrDefault(cusdue => cusdue.CustomerDueId == customerDueId);
        }
    }
}
