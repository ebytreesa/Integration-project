using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> SearchCustomer(Expression<Func<Customer, bool>> predicate);
        Task UpdateFromClient(Customer customer);
        Task UpdateSourceId(Customer customer);
    }
}
