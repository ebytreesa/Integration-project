using Domain.Models;
using Economic.Models;
using System.Collections.Generic;

namespace ConverterService.Customerconverter.Economic
{
    public interface IEconCustomerToCustomer
    {
        List<Customer> ToCustomerList(List<EconCustomer> ecustomers);
    }
}