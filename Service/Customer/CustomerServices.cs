using Model.Customer;
using System.Collections.Generic;
using Data.Repositories.Customer;

namespace Service.Customer
{
    public class CustomerServices : ICustomerServices
    {
        private readonly CustomerRepository customerRepository;

        public CustomerServices(CustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public ICollection<CustomerEntity> GetAllCustomers()
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
            var storedItem = customerRepository.GetById(customerId);

            if (storedItem != null)
            {
                storedItem.CustomerName = customerEntity.CustomerName;
                storedItem.PhoneNumber = customerEntity.PhoneNumber;
                storedItem.LocationId = customerEntity.LocationId;
                storedItem.CurrentDue = customerEntity.CurrentDue;
                storedItem.DueLimit = customerEntity.DueLimit;
                storedItem.TenantInfo.UserId = customerEntity.TenantInfo.UserId;

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
