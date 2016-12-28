using Model.Customer;
using System.Collections.Generic;

namespace Service.Customer
{
    public interface ICustomerServices
    {
        ICollection<CustomerEntity> GetAllCustomers();
        CustomerEntity GetCustomer(string customerId);
        CustomerEntity CreateCustomer(CustomerEntity customerEntity);
        bool UpdateCustomer(string customerId, CustomerEntity customerEntity);
        bool DeleteCustomer(string customerId);
    }
}
