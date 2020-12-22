using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDBProductService
    {
        Task Add(Product product);
        Task CUDDBProducts(List<Product> econproducts);
        Task<IEnumerable<Product>> GetAll();
        Task Update(Product product);
    }
}