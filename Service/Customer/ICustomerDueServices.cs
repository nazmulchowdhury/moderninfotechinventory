using Model.Customer;
using System.Collections.Generic;

namespace Service.Customer
{
    public interface ICustomerDueServices
    {
        ICollection<CustomerDueEntity> GetAllCustomerDues();
        CustomerDueEntity GetCustomerDue(string customerDueId);
        CustomerDueEntity CreateCustomerDue(CustomerDueEntity customerDueEntity);
        bool UpdateCustomerDue(string customerDueId, CustomerDueEntity customerDueEntity);
        bool DeleteCustomerDue(string customerDueId);
    }
}
