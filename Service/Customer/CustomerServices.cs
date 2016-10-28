using System.Collections.Generic;
using Data.Repositories.Customer;
using Model.Customer;

namespace Service.Customer
{
    public class CustomerServices : ICustomerServices
    {
        private readonly CustomerRepository customerRepository;

        public CustomerServices(CustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public IEnumerable<CustomerEntity> GetAllCustomers()
        {
            return customerRepository.GetAll();
        }

        public CustomerEntity GetCustomer(string customerId)
        {
            return customerRepository.GetById(customerId);
        }

        public CustomerEntity CreateCustomer(CustomerEntity customerEntity)
        {
            return customerRepository.Add(customerEntity);
        }

        public bool UpdateCustomer(string customerId, CustomerEntity customerEntity)
        {
            CustomerEntity storedItem = customerRepository.GetById(customerId);

            if (storedItem != null)
            {
                storedItem.CustomerName = customerEntity.CustomerName;
                storedItem.PhoneNumber = customerEntity.PhoneNumber;
                storedItem.LocationId = customerEntity.LocationId;
                storedItem.CurrentDue = customerEntity.CurrentDue;
                storedItem.DueLimit = customerEntity.DueLimit;
                customerRepository.Update(storedItem);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCustomer(string customerId)
        {
            return customerRepository.Delete(customerId);
        }
    }
}
