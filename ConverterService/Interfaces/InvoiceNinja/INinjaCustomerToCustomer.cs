using Domain.Models;
using InvoiceNinja.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConverterService
{
    public interface INinjaCustomerToCustomer
    {
        List<Customer> ToCustomerList(List<NinjaCustomerr> ninjaCustomers);
        //Task ToCustomerList(IEnumerable<NinjaCustomerr> nCustomers);
    }
}