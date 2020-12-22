using Economic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIService
{
    public class HttpClientHelper<T> : IHttpClientHelper<T>
    {

        private static readonly HttpClient Client = new HttpClient () ;

        public async Task<T> GetSingleItemRequest(string apiUrl, Dictionary<string, string> headres)

        {
            var result = default(T);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            foreach (var item in headres)
            {
                request.Headers.Add(item.Key, item.Value);
            }

            HttpResponseMessage response = await Client.SendAsync(request);


            //var response = await Client.GetAsync(apiUrl, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(data);

            }
            else
            {
                //var content = await response.Content.ReadAsStringAsync();
                //response.Content?.Dispose();
                // throw new HttpRequestException($"{response.StatusCode}:{content}");
                result = default(T);
            }
            return result;

        }


        public async Task<T> GetAllItemsRequest(string apiUrl, Dictionary<string, string> headres)
        {
            T result = default(T);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            foreach (var item in headres)
            {
                request.Headers.Add(item.Key, item.Value);
            }

            HttpResponseMessage response = await Client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
            return result;
        }


        public async Task<string> PostRequest(string apiUrl, Dictionary<string, string> headres, T postObject)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            foreach (var item in headres)
            {
                request.Headers.Add(item.Key, item.Value);
            }


           // var json = JsonConvert.SerializeObject(postObject);
            var json = ConvertToJson(postObject);

            

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.SendAsync(request);
            string responseAsString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
            return responseAsString;
        }

        private static string ConvertToJson(T postObject)
        {
            var json = JsonConvert.SerializeObject(postObject);
            

            if (postObject.GetType() == typeof(EconCustomer))
            {
                JObject jo = JObject.Parse(json);
                jo.Property("customerNumber").Remove();
                json = jo.ToString();

            }

            if (postObject.GetType() == typeof(EconCustomerContact))
            {
                JObject jo = JObject.Parse(json);
                jo.Property("customerContactNumber").Remove();
                json = jo.ToString();

            }

            if (postObject.GetType() == typeof(EconDeliveryLocation))
            {
                JObject jo = JObject.Parse(json);
                jo.Property("deliveryLocationNumber").Remove();
                json = jo.ToString();
            }
            
            return json;
        }

        public async Task<string> PutRequest(string apiUrl, Dictionary<string, string> headres, string putObject)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, apiUrl);
            foreach (var item in headres)
            {
                request.Headers.Add(item.Key, item.Value);
            }
            //request.Content = new StringContent(putObject, Encoding.UTF8, "application/json");
            var content = new StringContent(putObject, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(apiUrl, content);
            //HttpResponseMessage response = await Client.PutAsync(apiUrl, request.Content);

            string responseAsString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                
                throw new Exception();
            }
            return responseAsString;
        }


        public async Task DeleteRequest(string apiUrl, Dictionary<string, string> headres)
        {

            HttpRequestMessage request = new HttpRequestMessage();
            foreach (var item in headres)
            {
                request.Headers.Add(item.Key, item.Value);
            }
            //request.Content = new StringContent(putObject, Encoding.UTF8, "application/json");


            var response = await Client.DeleteAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
    }



}
