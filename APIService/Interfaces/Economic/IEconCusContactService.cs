using Economic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIService.ApiServices.Economic
{
    public interface IEconCusContactService
    {
        Task<List<EconCustomerContact>> CUDCustomerContacts(EconCustomer customer);
        Task<IEnumerable<EconCustomerContact>> GetAllCustomerContacts(string url);
        Task PostCustomerContacts(List<EconCustomerContact> econCusContacts);
    }
}