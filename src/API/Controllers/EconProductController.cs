using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIService.ApiServices.Economic;
using Domain.Interfaces;
using Domain.Models;
using Economic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EconProductController : ControllerBase
    {
        private readonly IEconProductService _economicProductService;
        private readonly IDBProductService _dBProductService;

        public EconProductController(IEconProductService econProductService,
                       IDBProductService dBProductService)
        {
            _economicProductService = econProductService;
            _dBProductService = dBProductService;
        }


        // GET: api/EconProduct
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                var result = await _dBProductService.GetAll();
                return result.Where(x => x.source == "E-conomic");
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
                await _economicProductService.PostOrUpdateData(products);
                return RedirectToAction("Get", "NinjaProduct");
            }
            catch (Exception)
            {

                throw;
            }
            

        }
    }
}