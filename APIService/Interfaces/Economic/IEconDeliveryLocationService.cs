using Economic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIService.ApiServices.Economic
{
    public interface IEconDeliveryLocationService
    {
        Task<IEnumerable<EconDeliveryLocation>> GetAllDeliveryLocations(string url);
        Task PostDeliveryLocations(List<EconDeliveryLocation> econDeliveryLocations);
    }
}