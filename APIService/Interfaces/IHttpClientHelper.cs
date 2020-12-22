using InvoiceNinja.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIService
{
    public interface IHttpClientHelper<T>
    {
        Task DeleteRequest(string apiUrl, Dictionary<string, string> headres);
        Task<T> GetAllItemsRequest(string apiUrl, Dictionary<string, string> headres);
        Task<T> GetSingleItemRequest(string apiUrl, Dictionary<string, string> headres);
        Task<string> PostRequest(string apiUrl, Dictionary<string, string> headres, T postObject);
        Task<string> PutRequest(string apiUrl, Dictionary<string, string> headres, string putObject);
    }
}