using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIService.ApiServices;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EconCustomerController : ControllerBase    {


        private readonly IEconomicCustomerService _economicCustomerService;
        private readonly ICustomerService _customerService;

        public EconCustomerController(IEconomicCustomerService economicCustomerService,
            ICustomerService customerService
            )
        {
            _economicCustomerService = economicCustomerService;
            _customerService = customerService;
        }
        // GET: api/EconCustomer
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            try
            {
                var result =  await _customerService.GetAll();
                return result.Where(x => x.CustomerSource == "E-conomic");
            }
            catch (Exception)
            {

                throw;
            }
            
        }     

        // POST: api/EconCustomer
        [HttpPost]
        public async Task<IActionResult> Post(List<Customer> customers)
        {
            try
            {
                await _economicCustomerService.PostOrUpdateData(customers);
                return RedirectToAction("Get", "EconCustomer");
            }
            catch (Exception)
            {

                throw;
            }
            

        }
        // PUT: api/EconCustomer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
