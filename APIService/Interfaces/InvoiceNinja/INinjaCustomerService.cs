using Domain.Models;
using InvoiceNinja.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIService.ApiServices
{
    public interface INinjaCustomerService 
    {
        Task<IEnumerable<NinjaCustomerr>> ReadCustomerData();
        Task PostOrUpdateData(List<Customer> customers);
    }
}