using System.Collections.Generic;
using Model.Customer;

namespace Service.Customer
{
    public interface ICustomerServices
    {
        IEnumerable<CustomerEntity> GetAllCustomers();
        CustomerEntity GetCustomer(string customerId);
        CustomerEntity CreateCustomer(CustomerEntity customerEntity);
        bool UpdateCustomer(string customerId, CustomerEntity customerEntity);
        bool DeleteCustomer(string customerId);
    }
}
