using APIService.ApiServices;
using APIService.ApiServices.Economic;
using APIService.ApiServices.InvoiceNinja;
using ConverterService;
using ConverterService.Customerconverter.Economic;
using ConverterService.ProductConverter.Economic;
using ConverterService.ProductConverter.InvoiceNinja;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class BackgroundService
    {
        // API services
        private readonly INinjaCustomerService _ninjaCustomerService;
        private readonly IEconomicCustomerService _economicCustomerService;

        private readonly IEconProductService _econProductService;
        private readonly INinjaProductService _ninjaProductService;

        // converter services
        private readonly INinjaCustomerToCustomer _ninjaCustomerToCustomer;
        private readonly IEconCustomerToCustomer _econCustomerToCustomer;

        private readonly IEconProductsToProducts _econProdutsToProducts;
        private readonly INinjaProductsToProducts _ninjaProductsToProducts;

        //DatabaseServices
        private readonly ICustomerService _customerService;
        private readonly IDBProductService _dBProductService;

        public BackgroundService(
            INinjaCustomerService ninjaCustomerService,
            IEconomicCustomerService economicCustomerService,
            INinjaCustomerToCustomer ninjaCustomerToCustomer,
            IEconCustomerToCustomer econCustomerToCustomer,
            ICustomerService customerService,
            IEconProductService econProductService,
            INinjaProductService ninjaProductService,
            IDBProductService dBProductService,
            IEconProductsToProducts econProductsToProducts,
            INinjaProductsToProducts ninjaProductsToProducts
            )
        {
          
            _ninjaCustomerService = ninjaCustomerService;
            _economicCustomerService = economicCustomerService;
            _econCustomerToCustomer = econCustomerToCustomer;
            _ninjaCustomerToCustomer = ninjaCustomerToCustomer;
            _customerService = customerService;
            _econProductService = econProductService;
            _ninjaProductService = ninjaProductService;
            _dBProductService = dBProductService;
            _econProdutsToProducts = econProductsToProducts;
            _ninjaProductsToProducts = ninjaProductsToProducts;
        }

        public void test()
        {
            Console.WriteLine("apple pie");
        }
        public async Task BackgroundJobs()
        {
            var eCustomers = await _economicCustomerService.ReadCustomerData();
            var cusFromEconomic = _econCustomerToCustomer.ToCustomerList(eCustomers.ToList());
            await _customerService.CUD(cusFromEconomic);

            var nCustomers = await _ninjaCustomerService.ReadCustomerData();
            var cusFromNinja = _ninjaCustomerToCustomer.ToCustomerList(nCustomers.ToList());
            await _customerService.CUD(cusFromNinja);

            var nProducts = await _ninjaProductService.ReadProductData();
            var productsFromNinja = _ninjaProductsToProducts.ToProducts(nProducts);
            await _dBProductService.CUDDBProducts(productsFromNinja);

            var eProducts = await _econProductService.ReadProductData();
            var productsFromEcon = _econProdutsToProducts.ToProductList(eProducts);
            await _dBProductService.CUDDBProducts(productsFromEcon);


            //return nCustomers;

        }
    }
}
