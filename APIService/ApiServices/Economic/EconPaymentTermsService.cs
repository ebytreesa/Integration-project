using Economic;
using Economic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIService.ApiServices.Economic
{
    public class EconPaymentTermsService : IEconPaymentTermsService
    {

        private readonly IHttpClientHelper<EconPaymentTermsApiModel> _httpClientHelper;
        private readonly IHttpClientHelper<EconPaymentTerms> _httpClientHelper1;
        public EconPaymentTermsService(IHttpClientHelper<EconPaymentTermsApiModel> httpClientHelper,
         IHttpClientHelper<EconPaymentTerms> httpClientHelper1 )
        {
            _httpClientHelper = httpClientHelper;
            _httpClientHelper1 = httpClientHelper1;
        }
        public async Task<EconPaymentTerms> GetPaymentTerms(string url)
        {
            var result = await _httpClientHelper1.GetSingleItemRequest(url, EconApiHelper.headerTokens);
            return result;
        }

        public async Task<EconPaymentTerms> GetOrCreateEconPaymentTerms(EconPaymentTerms econPaymentTerm)
        {

            var filterUrl = $"{EconApiHelper.baseUrl}payment-terms?filter=daysOfCredit$eq:{econPaymentTerm.daysOfCredit}$and:paymentTermsType$eq:net";
            var result = await _httpClientHelper.GetSingleItemRequest(filterUrl, EconApiHelper.headerTokens);
            //if the payment terms doesn't exists
            if (result.collection.Count == 0)
            {
                econPaymentTerm = PostEconPaymentTerm(econPaymentTerm).Result;
            }
            else // if it exists
            {
                econPaymentTerm = result.collection.FirstOrDefault();

            }
            return econPaymentTerm;
        }

        public async Task<EconPaymentTerms> PostEconPaymentTerm(EconPaymentTerms econPaymentTerm)
        {
            var url = $"{EconApiHelper.baseUrl}payment-terms";
            var paymentTerms = new EconPaymentTerms()
            {
                name = "Netto " + econPaymentTerm.daysOfCredit + " dage",
                paymentTermsType = "net",
                daysOfCredit = econPaymentTerm.daysOfCredit,
            };
            string result = await _httpClientHelper1.PostRequest(url, EconApiHelper.headerTokens, paymentTerms);
            var deserialized = JsonConvert.DeserializeObject<EconPaymentTerms>(result);
            paymentTerms.paymentTermsNumber = deserialized.paymentTermsNumber;
            paymentTerms.self = "https://restapi.e-conomic.com/payment-terms/" + deserialized.paymentTermsNumber;
            return paymentTerms;
        }



    }
}
