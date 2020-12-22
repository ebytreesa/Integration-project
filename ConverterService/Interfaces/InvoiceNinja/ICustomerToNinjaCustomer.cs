using Domain.Models;
using InvoiceNinja.Models;
using System.Collections.Generic;

namespace ConverterService.Customerconverter
{
    public interface ICustomerToNinjaCustomer
    {
        List<NinjaCustomerr> ToNinjaCustomerList(List<Customer> customers);
    }
}