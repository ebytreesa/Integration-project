using Domain.Models;
using Economic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIService.ApiServices
{
    public interface IEconomicCustomerService
    {
        Task PostOrUpdateData(List<Customer> customers);
        Task<IEnumerable<EconCustomer>> ReadCustomerData();
    }
}