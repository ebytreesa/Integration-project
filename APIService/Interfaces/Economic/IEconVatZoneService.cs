using Economic.Models;
using System.Threading.Tasks;

namespace APIService.ApiServices.Economic
{
    public interface IEconVatZoneService
    {
        Task<EconVatzone> GetVatZone(string url);
    }
}