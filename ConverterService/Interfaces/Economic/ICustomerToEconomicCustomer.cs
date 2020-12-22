using Domain.Models;
using Economic.Models;
using System.Collections.Generic;

namespace ConverterService.Customerconverter.Economic
{
    public interface ICustomerToEconomicCustomer
    {
       // List<EconCustomer> ConvertToTypeCustomers(List<Customer> customers);
        List<EconCustomer> ToEconCustomerList(List<Customer> customers);
    }
}