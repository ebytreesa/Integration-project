using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APIService;
using APIService.ApiServices;
using Domain.Interfaces;
using Domain.Models;
using InvoiceNinja.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class NinjaCustomerController : ControllerBase
    {
        private readonly INinjaCustomerService _ninjaCustomerService;
        private readonly ICustomerService _customerService;

        public NinjaCustomerController(INinjaCustomerService ninjaCustomerService,
            ICustomerService customerService)
        {
            _ninjaCustomerService = ninjaCustomerService;
            _customerService = customerService;
        }
       

       // GET: api/NinjaCustomer
       [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            try
            {
                var result = await _customerService.GetAll();
                return result.Where(x => x.CustomerSource == "Invoice Ninja");
            }
            catch (Exception)
            {

                throw;
            }
           
        }



        // POST: api/NinjaCustomer
        [HttpPost]
        public async Task<IActionResult> Post(List<Customer> customers)  
        {
            try
            {
                await _ninjaCustomerService.PostOrUpdateData(customers);
                return RedirectToAction("GetCutomers", "Customers");
            }
            catch (Exception)
            {

                throw;
            }
           
           
        }

       
    }
}
