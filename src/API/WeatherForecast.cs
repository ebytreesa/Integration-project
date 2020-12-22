using APIService.ApiServices;
using ConverterService;
using ConverterService.Customerconverter.Economic;
using Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public abstract class DataHandler
    {
       public  abstract  Task FetchAndSave();
    }

    public class NinjaCustomerHandler : DataHandler
    {
        private readonly INinjaCustomerService _ninjaCustomerService;
        private readonly INinjaCustomerToCustomer _ninjaCustomerToCustomer;
        private readonly ICustomerService _customerService;

        public NinjaCustomerHandler(INinjaCustomerService ninjaCustomerService,
            INinjaCustomerToCustomer ninjaCustomerToCustomer,
            ICustomerService customerService
            )
        {
            _ninjaCustomerService = ninjaCustomerService;
            _ninjaCustomerToCustomer = ninjaCustomerToCustomer;
            _customerService = customerService;
        }
        public async override Task FetchAndSave()
        {
            var nCustomers = await _ninjaCustomerService.ReadCustomerData();
            var cusFromNinja = _ninjaCustomerToCustomer.ToCustomerList(nCustomers.ToList());
            await _customerService.CUD(cusFromNinja);
        }
    }

    public class EconCustomerHandler : DataHandler
    {
        private readonly IEconomicCustomerService _economicCustomerService;
        private readonly IEconCustomerToCustomer _econCustomerToCustomer;
        private readonly ICustomerService _customerService;
        public EconCustomerHandler(IEconomicCustomerService economicCustomerService,
            IEconCustomerToCustomer econCustomerToCustomer,
            ICustomerService customerService
            )
        {
            _economicCustomerService = economicCustomerService;
            _econCustomerToCustomer = econCustomerToCustomer;
            _customerService = customerService;
        }


        public override async Task FetchAndSave()
        {
            var eCustomers = await _economicCustomerService.ReadCustomerData();
            var cusFromEconomic = _econCustomerToCustomer.ToCustomerList(eCustomers.ToList());
            await _customerService.CUD(cusFromEconomic);
        }
    }
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
