using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface  ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(Customer Customer);
        Task Remove(Customer Customer);
        Task CUDCustomers(List<Customer> customers);
        Task CUD(List<Customer> customerList);
    }
}
