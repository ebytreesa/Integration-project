using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SQLDB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SQLDB.Repositories
{
   public class CustomerRepository : Repository<Customer>, ICustomerRepository
   {
        public CustomerRepository(CustomerDbContext context) : base(context) { }

        public override async Task<List<Customer>> GetAll()
        {
            return await _customerDbContext.Set<Customer>().AsNoTracking()
                .Include(c => c.customerContacts)
                .Include(c => c.ShippingAddress)
                .ToListAsync();
        }

        public async Task UpdateSourceId(Customer customer)
        {
            _customerDbContext.Set<Customer>().Update(customer);
            await SaveChanges();


        }
        public override async Task Update(Customer customer)
        {
            try
            {
                _customerDbContext.Set<Customer>().Update(customer);
                foreach (var item in customer.customerContacts)
                {
                    _customerDbContext.Entry(item).State = EntityState.Detached;
                }
                foreach (var item in customer.ShippingAddress)
                {
                    _customerDbContext.Entry(item).State = EntityState.Detached;
                }

                _customerDbContext.Entry(customer).Property(x => x.CustomerSource).IsModified = false;
                _customerDbContext.Entry(customer).Property(x => x.customerSourceId).IsModified = false;

                //_customerDbContext.Entry(customer).State = EntityState.Modified;
                await SaveChanges();

            }
            catch (Exception)
            {
                throw;
                
            }
            
           
        }

        public async Task UpdateninjaCus(Customer customer)
        {

            _customerDbContext.Set<Customer>().Update(customer);
            foreach (var item in customer.customerContacts)
            {
                _customerDbContext.Entry(item).State = EntityState.Detached;
            }
            foreach (var item in customer.ShippingAddress)
            {
                _customerDbContext.Entry(item).State = EntityState.Detached;
            }
            
            if (customer.EconomicCustomerId != 0)
            {
                _customerDbContext.Entry(customer).Property(x => x.EconomicCustomerId).IsModified = false;

            }
            _customerDbContext.Entry(customer).Property(x => x.CustomerSource).IsModified = false;
            _customerDbContext.Entry(customer).Property(x => x.customerSourceId).IsModified = false;
            // _customerDbContext.Entry(customer).State = EntityState.Modified;
            await SaveChanges();
        }


        public async Task UpdateFromClient(Customer customer)
        {
            _customerDbContext.Set<Customer>().Update(customer);
            foreach (var item in customer.customerContacts)
            {
                _customerDbContext.Entry(item).State = EntityState.Detached;
            }
            foreach (var item in customer.ShippingAddress)
            {
                _customerDbContext.Entry(item).State = EntityState.Detached;
            }
            _customerDbContext.Entry(customer).Property(x => x.CustomerSource).IsModified = false;
            _customerDbContext.Entry(customer).Property(x => x.customerSourceId).IsModified = false;


            await SaveChanges();


        }

        public async Task<Customer> SearchCustomer(Expression<Func<Customer, bool>> predicate)
        {
            return await _customerDbContext.Set<Customer>()
                         .Where(predicate)
                         .AsNoTracking()
                         .Include(p => p.customerContacts)
                         .AsNoTracking()
                         .Include(p => p.ShippingAddress)
                         .AsNoTracking()
                         .FirstOrDefaultAsync();

        }

        //public override async Task<Customer> GetById(int id)
        //{
        //    return await _customerDbContext.Set<Customer>().AsNoTracking()
        //        .Where(b => b.customerSourceId == id)
        //        .Include(c => c.customerContacts)
        //        .Include(c => c.ShippingAddress)
        //        .FirstOrDefaultAsync();
        //}
    }
}


            //if (customer.InvoiceNinjaCustomerId != 0)
            //{
            //    _customerDbContext.Entry(customer).Property(x => x.InvoiceNinjaCustomerId).IsModified = false;

            //}
            //if (customer.EconomicCustomerId != 0)
            //{
            //    _customerDbContext.Entry(customer).Property(x => x.EconomicCustomerId).IsModified = false;

            //}