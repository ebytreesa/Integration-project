using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CustomerContactService : ICustomerContactService
    {

        private readonly ICustomerContactRepository _customerContactRepository;

        public CustomerContactService(ICustomerContactRepository customerContactRepository)
        {
            _customerContactRepository = customerContactRepository;
        }
        public Task<CustomerContact> Add(CustomerContact customerContact)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerContact>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerContact> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(CustomerContact customerContact)
        {
            await _customerContactRepository.Remove(customerContact);
        }

        public async Task<CustomerContact> Update(CustomerContact customerContact)
        {
            await _customerContactRepository.Update(customerContact);
            return customerContact;
        }

        public async Task CUDCustomerContacts(Customer cus, List<CustomerContact> contacts)
        {

            //delete rows from db which is not in the new list
            var dbList = await _customerContactRepository.Search(c => c.customer.CustomerId == cus.CustomerId);

            foreach (var existingCusContact in dbList)
            {
                if (!contacts.Any(c => c.customerContactNumber == existingCusContact.customerContactNumber))
                {
                   await  Remove(existingCusContact);
                }

            }

            //add or update customer contacts
            foreach (var item in contacts)
            {

                var existingContact = _customerContactRepository.Search(c => c.customerContactNumber == item.customerContactNumber
                && c.customer.CustomerId == item.customer.CustomerId).Result.FirstOrDefault();

                if (existingContact != null)
                {
                    item.customerContactId = existingContact.customerContactId;
                    await _customerContactRepository.Update(item);
                }
                else
                {
                    await _customerContactRepository.Add(item);
                }
            }
        }
        
    }
}
//Delete customer contact
//var dbContactlist =  await _customerContactRepository.Search(c => c.customerNumber == cus.CustomerId);
//foreach (var item in dbContactlist)
//    {
//        if (!contacts.Any(c => c.customerContactNumber == item.customerContactNumber))
//        {
//            //await _customerContactRepository.Remove(item);
//            await Remove(item);


//        }

//    }
