using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerContactService
    {
        Task<IEnumerable<CustomerContact>> GetAll();
        Task<CustomerContact> GetById(int id);
        Task<CustomerContact> Add(CustomerContact customerContact);
        Task<CustomerContact> Update(CustomerContact customerContact);
        Task Remove(CustomerContact customerContact);
        Task CUDCustomerContacts(Customer cus, List<CustomerContact> contacts);
    }
}
