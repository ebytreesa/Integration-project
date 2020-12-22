using ConverterService.Customerconverter;
using Domain.Interfaces;
using Domain.Models;
using InvoiceNinja;
using InvoiceNinja.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIService.ApiServices
{
    public class NinjaCustomerService : INinjaCustomerService
    {
        private readonly IHttpClientHelper<NinjaCustomerApiModel> _httpClientHelper;
        private readonly IHttpClientHelper<NinjaCustomerr> _ninjaHttpClientHelper;
        private readonly ICustomerToNinjaCustomer _customerToNinjaCustomer;
        private readonly ICustomerRepository _customerRepository;
        public NinjaCustomerService(IHttpClientHelper<NinjaCustomerApiModel> httpClientHelper,
            IHttpClientHelper<NinjaCustomerr> ninjaHttpClientHelper,
            ICustomerToNinjaCustomer customerToNinjaCustomer,
            ICustomerRepository customerRepository)
        {
            _httpClientHelper = httpClientHelper;
            _ninjaHttpClientHelper = ninjaHttpClientHelper;
            _customerToNinjaCustomer = customerToNinjaCustomer;
            _customerRepository = customerRepository;
        }
        public async Task<IEnumerable<NinjaCustomerr>> ReadCustomerData()
        {
            int currentPage = 1;
            int totalPages = 0;
            var result = new List<NinjaCustomerr>();
            do
            {
                var nextUrl = $"{NinjaApiHelper.baseUrl}clients?page={currentPage}";
                //HttpClientHelper<NinjaCustomerApiModel> httpClientHelper = new HttpClientHelper<NinjaCustomerApiModel>();
                var resul = await _httpClientHelper.GetAllItemsRequest(nextUrl, NinjaApiHelper.headerTokens);
                totalPages = resul.meta.pagination.total_pages;
                currentPage++;
                result.AddRange(resul.data);

            } while (currentPage <= totalPages);

            return result.Where(x => x.is_deleted == false).ToList();
        }

        private async Task<NinjaCustomerr> GetCustomerById(int id)
        {
            var url = $"{NinjaApiHelper.baseUrl}clients/{id}";
            return await _ninjaHttpClientHelper.GetSingleItemRequest(url, NinjaApiHelper.headerTokens);            

        }


        public async Task PostOrUpdateData(List<Customer> customers)
        {
            
            //Convert customer to ninjaCustomer
            var ninjaCustomers = _customerToNinjaCustomer.ToNinjaCustomerList(customers);
            var apiCusList = await ReadCustomerData();

            var listToUpdate = ninjaCustomers.Where(a => apiCusList.Any(b => b.id == a.id)).ToList();
            var listToAdd = ninjaCustomers.Where(c => apiCusList.All(d => d.id != c.id)).ToList();


            //post to ninja api
            foreach (var item in listToAdd)
            {
                if (item != null)
                {
                  int id =  await PostNinjaCustomer(item);
                  //update db row with new invoice ninja customer id
                  var dbCus =   _customerRepository.Search(x => x.CustomerName == item.name).Result.FirstOrDefault();
                  dbCus.InvoiceNinjaCustomerId = id;
                  await _customerRepository.UpdateFromClient(dbCus);
                }
            }

            //update ninja customer
            foreach (var item in listToUpdate)
            {
               // var existingCus = await GetCustomerByName(item.name);
                await UpdateNinjaCustomer(item);
            }
        }

        private async Task<string> UpdateNinjaCustomer(NinjaCustomerr item)
        {
            var url = $"{NinjaApiHelper.baseUrl}clients/{item.id}" ;
            var json = JsonConvert.SerializeObject(item);
            return await _ninjaHttpClientHelper.PutRequest(url, NinjaApiHelper.headerTokens, json);
        }

        private async Task<int> PostNinjaCustomer(NinjaCustomerr item)
        {
            var url = $"{NinjaApiHelper.baseUrl}clients"; ;
            var json =  await _ninjaHttpClientHelper.PostRequest(url, NinjaApiHelper.headerTokens, item);
            var obj = JsonConvert.DeserializeObject<NinjaCusData>(json);
            return obj.data.id;

        }
    }
}

    

