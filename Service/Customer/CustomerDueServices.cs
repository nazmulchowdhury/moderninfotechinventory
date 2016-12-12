using System.Collections.Generic;
using Data.Repositories.Customer;
using Model.Customer;

namespace Service.Customer
{
    public class CustomerDueServices : ICustomerDueServices
    {
        private readonly CustomerDueRepository customerDueRepository;

        public CustomerDueServices(CustomerDueRepository customerDueRepository)
        {
            this.customerDueRepository = customerDueRepository;
        }

        public ICollection<CustomerDueEntity> GetAllCustomerDues()
        {
            return customerDueRepository.GetAll();
        }

        public CustomerDueEntity GetCustomerDue(string customerDueId)
        {
            return customerDueRepository.GetById(customerDueId);
        }

        public CustomerDueEntity CreateCustomerDue(CustomerDueEntity customerDueEntity)
        {
            return customerDueRepository.Add(customerDueEntity);
        }

        public bool UpdateCustomerDue(string customerDueId, CustomerDueEntity customerDueEntity)
        {
            var storedItem = customerDueRepository.GetById(customerDueId);

            if (storedItem != null)
            {
                storedItem.ReceiveAmount = customerDueEntity.ReceiveAmount;
                storedItem.CustomerId = customerDueEntity.CustomerId;

                customerDueRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCustomerDue(string customerDueId)
        {
            return customerDueRepository.Delete(customerDueId);
        }
    }
}
