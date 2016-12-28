using Model.Customer;
using Data.Infrastructure;

namespace Data.Repositories.Customer
{
    public interface ICustomerRepository : IRepository<CustomerEntity>
    { }
}
