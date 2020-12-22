using Domain.Models;
using InvoiceNinja.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIService.ApiServices.InvoiceNinja
{
    public interface INinjaProductService
    {
        Task PostOrUpdateData(List<Product> products);
        Task<List<NinjaProduct>> ReadProductData();
    }
}