using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using SQLDB.Context;
using Domain.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService  _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]        
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var r = await _customerService.GetAll();
                return Ok(r);
            }
            catch (Exception)
            {
                throw;
            }
            
            
        }


        [HttpPost]
        public async Task<ActionResult<List<Customer>>> PostCustomers(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                await _customerService.Add(customer);

            }
            try
            {
                return CreatedAtAction("GetCustomers", customers);
            }
            catch (Exception)
            {

                throw;
            }

           
        }

        
    }
}



//// PUT: api/Customers/5
//// To protect from overposting attacks, enable the specific properties you want to bind to, for
//// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
//[HttpPut("{id}")]
//public async Task<IActionResult> PutCustomer(int id, Customer customer)
//{
//    if (id != customer.CustomerId)
//    {
//        return BadRequest();
//    }

//    _context.Entry(customer).State = EntityState.Modified;

//    try
//    {
//        await _context.SaveChangesAsync();
//    }
//    catch (DbUpdateConcurrencyException)
//    {
//        if (!CustomerExists(id))
//        {
//            return NotFound();
//        }
//        else
//        {
//            throw;
//        }
//    }

//    return NoContent();
//}

// POST: api/Customers
// To protect from overposting attacks, enable the specific properties you want to bind to, for
// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
