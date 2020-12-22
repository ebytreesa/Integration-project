using APIService;
using APIService.ApiServices;
using APIService.ApiServices.Economic;
using APIService.ApiServices.InvoiceNinja;
using ConverterService;
using ConverterService.Customerconverter;
using ConverterService.Customerconverter.Economic;
using ConverterService.ProductConverter.Economic;
using ConverterService.ProductConverter.InvoiceNinja;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SQLDB.Context;
using SQLDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Configuration
{
    public static class DependencyInjenctionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INinjaCustomerService, NinjaCustomerService>();
            services.AddScoped<IEconomicCustomerService, EconCustomerService>();
            services.AddScoped<IEconCusContactService, EconCusContactService>();
            services.AddScoped<IEconPaymentTermsService, EconPaymentTermsService>();
            services.AddScoped<IEconVatZoneService, EconVatZoneService>();
            services.AddScoped<IEconDeliveryLocationService, EconDeliveryLocationService>();

            services.AddScoped<IEconProductService, EconProductService>();
            services.AddScoped<INinjaProductService, NinjaProductService>();


            services.AddScoped<INinjaCustomerToCustomer, NinjaCustomerToCustomer>();
            services.AddScoped<IEconCustomerToCustomer, EconCustomerToCustomer>();
            services.AddScoped<ICustomerToEconomicCustomer, CustomerToEconomicCustomer>();
            services.AddScoped<ICustomerToNinjaCustomer, CustomerToNinjaCustomer>();
            
            services.AddScoped<IEconProductsToProducts, EconProductsToProducts>();
            services.AddScoped<INinjaProductsToProducts, NinjaProductsToProducts>();
            services.AddScoped<IProductsToEconProducts, ProductsToEconProducts>();
            services.AddScoped<IProductsToNinjaProducts, ProductsToNinjaProducts>();


           services.AddScoped<DbContext, CustomerDbContext>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerContactRepository, CustomerContactRepository>();
            services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerContactService, CustomerContactService>();
            services.AddScoped<IShippingAddressService, ShippingAddressService>();

            services.AddScoped<IDBProductService, DBProductService>();



            services.AddScoped(typeof(IHttpClientHelper<>), typeof(HttpClientHelper<>));

            return services;
        }
    }
}
