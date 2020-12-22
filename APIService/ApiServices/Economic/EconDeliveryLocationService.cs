using Economic;
using Economic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIService.ApiServices.Economic
{
    public class EconDeliveryLocationService : IEconDeliveryLocationService
    {

        private readonly IHttpClientHelper<EconDeliveryLocationApiModel> _httpClientHelper;
        private readonly IHttpClientHelper<EconDeliveryLocation> _httpClientHelper1;
        public EconDeliveryLocationService(IHttpClientHelper<EconDeliveryLocationApiModel> httpClientHelper,
            IHttpClientHelper<EconDeliveryLocation> httpClientHelper1)
        {
            _httpClientHelper = httpClientHelper;
            _httpClientHelper1 = httpClientHelper1;
        }
        public async Task<IEnumerable<EconDeliveryLocation>> GetAllDeliveryLocations(string url)
        {
            var result = await _httpClientHelper.GetAllItemsRequest(url, EconApiHelper.headerTokens);
            return result.collection;
        }

        public async Task PostDeliveryLocations(List<EconDeliveryLocation> econDeliveryLocations)
        {
            foreach (var item in econDeliveryLocations)
            {
                var url = $"{EconApiHelper.baseUrl}customers/{item.customer.customerNumber}/delivery-locations";
                await _httpClientHelper1.PostRequest(url, EconApiHelper.headerTokens, item);
            }
        }
    }
}
