using Domain.Models;
using Economic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIService.ApiServices.Economic
{
    public interface IEconProductService
    {
        Task PostOrUpdateData(List<Product> products);
        Task<List<EconProduct>> ReadProductData();
    }
}