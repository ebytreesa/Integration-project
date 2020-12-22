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
    public class EconCusContactService : IEconCusContactService
    {
        private readonly IHttpClientHelper<EconCusContactApiModel> _httpClientHelper;
        private readonly IHttpClientHelper<EconCustomerContact> _httpClientHelper1;
        public EconCusContactService(IHttpClientHelper<EconCusContactApiModel> httpClientHelper,
            IHttpClientHelper<EconCustomerContact> httpClientHelper1)
        {
            _httpClientHelper = httpClientHelper;
            _httpClientHelper1 = httpClientHelper1;
        }
        public async Task<IEnumerable<EconCustomerContact>> GetAllCustomerContacts(string url)
        {           
            var result = await _httpClientHelper.GetAllItemsRequest(url, EconApiHelper.headerTokens);
            return result.collection;
        }

        public  async Task PostCustomerContacts(List<EconCustomerContact> econCusContacts)
        {
            foreach (var item in econCusContacts)
            {
                var url = $"{EconApiHelper.baseUrl}customers/{item.customer.customerNumber}/contacts";
                await _httpClientHelper1.PostRequest(url, EconApiHelper.headerTokens, item);
            }
        }

        public  async Task<List<EconCustomerContact>> CUDCustomerContacts(EconCustomer customer)
        {
            var   cc = new List<EconCustomerContact>();
            // Get all customer contacts from api           
            var url = $"{EconApiHelper.baseUrl}customers/{customer.customerNumber}/contacts"; 
            var customerContacts = GetAllCustomerContacts(url).Result;

            //Update and Insert contacts
            foreach (var contact in customer.econCustomerContacts)
            {
                contact.customer.customerNumber = customer.customerNumber;
                var existingContact = customerContacts.Where(x => x.name == contact.name).FirstOrDefault();
                if (existingContact != null)
                {
                    //update
                    contact.customerContactNumber = existingContact.customerContactNumber;
                    cc.Add(await UpdateContact(contact));
                }
                else
                {
                    //insert
                   cc.Add(await InsertContact(contact));
                }

            }
            // Delete items from e-conomic  from which are not in list

            foreach (var item in customerContacts)
            {
                bool b = customer.econCustomerContacts.Any(x => x.name == item.name);
                if (!b)
                {
                    //Delete item
                    if (item.isPrimary == true)
                    {
                        item.isPrimary = false; // because primary contacts are attention in e-conomic customer
                                                // and cannot be deleted
                    }
                    await DeleteContact(item);

                }
            }
            return cc;
            
        }

      


       

        private static async Task DeleteContact(EconCustomerContact contact)
        {
            HttpClientHelper<EconCustomerContact> httpClientHelper = new HttpClientHelper<EconCustomerContact>();
            var url = $"{EconApiHelper.baseUrl}customers/{contact.customer.customerNumber}/contacts/{contact.customerContactNumber}";
            await httpClientHelper.DeleteRequest(url, EconApiHelper.headerTokens);
        }

        private static async Task<EconCustomerContact> UpdateContact(EconCustomerContact contact)
        {
            HttpClientHelper<EconCustomerContact> httpClientHelper = new HttpClientHelper<EconCustomerContact>();
            var url = $"{EconApiHelper.baseUrl}customers/{contact.customer.customerNumber}/contacts/{contact.customerContactNumber}";
            var json = JsonConvert.SerializeObject(contact);
            var result = await httpClientHelper.PutRequest(url, EconApiHelper.headerTokens, json);
            var obj = JsonConvert.DeserializeObject<EconCustomerContact>(result);
            return obj;
        }

        private static async Task<EconCustomerContact> InsertContact(EconCustomerContact contact)
        {
            HttpClientHelper<EconCustomerContact> httpClientHelper = new HttpClientHelper<EconCustomerContact>();
            var url = $"{EconApiHelper.baseUrl}customers/{contact.customer.customerNumber}/contacts";
            var result = await httpClientHelper.PostRequest(url, EconApiHelper.headerTokens, contact);
            var obj = JsonConvert.DeserializeObject<EconCustomerContact>(result);
            return obj;
        }
    }
}
