using System.Collections.Generic;
using Model.Customer;

namespace Service.Customer
{
    public interface ICustomerDueServices
    {
        IEnumerable<CustomerDueEntity> GetAllCustomerDues();
        CustomerDueEntity GetCustomerDue(string customerDueId);
        CustomerDueEntity CreateCustomerDue(CustomerDueEntity customerDueEntity);
        bool UpdateCustomerDue(string customerDueId, CustomerDueEntity customerDueEntity);
        bool DeleteCustomerDue(string customerDueId);
    }
}
