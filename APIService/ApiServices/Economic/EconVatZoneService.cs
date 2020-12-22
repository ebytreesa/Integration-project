using Economic;
using Economic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIService.ApiServices.Economic
{
    public class EconVatZoneService : IEconVatZoneService
    {
        private readonly IHttpClientHelper<EconVatzone> _httpClientHelper;
        public EconVatZoneService(IHttpClientHelper<EconVatzone> httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }
        public async Task<EconVatzone> GetVatZone(string url)
        {
            var result = await _httpClientHelper.GetAllItemsRequest(url, EconApiHelper.headerTokens);
            return result;
        }
    }
}
