using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIService.ApiServices.InvoiceNinja;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NinjaProductController : ControllerBase
    {
        private readonly INinjaProductService _ninjaProductService;
        private readonly IDBProductService _dBProductService;
        public NinjaProductController(INinjaProductService ninjaProductService,
                       IDBProductService dBProductService)
        {
            _ninjaProductService = ninjaProductService;
            _dBProductService = dBProductService;
        }


        // GET: api/EconProduct
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                var result = await _dBProductService.GetAll();
                return result.Where(x => x.source == "Invoice Ninja");
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<Product> products)
        {
            try
            {
                await _ninjaProductService.PostOrUpdateData(products);
                return RedirectToAction("Get", "EconProduct");
            }
            catch (Exception)
            {

                throw;
            }
            

        }

    }
}