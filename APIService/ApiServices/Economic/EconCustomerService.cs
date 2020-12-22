using APIService.ApiServices.Economic;
using ConverterService.Customerconverter.Economic;
using Domain.Interfaces;
using Domain.Models;
using Economic;
using Economic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIService.ApiServices
{
    public class EconCustomerService : IEconomicCustomerService
    {
        private readonly IHttpClientHelper<EconCustomerApiModel> _httpClientHelper;
        private readonly IHttpClientHelper<EconCustomer> _econHttpClientHelper;
        private readonly IEconCusContactService _econCusContactService;
        private readonly ICustomerToEconomicCustomer _customerToEconCustomer;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEconPaymentTermsService _econPaymentTermsService;
        private readonly IEconVatZoneService _econVatZoneService;
        private readonly IEconDeliveryLocationService _econDeliveryLocationService;
        public EconCustomerService(IHttpClientHelper<EconCustomerApiModel> httpClientHelper,
            IHttpClientHelper<EconCustomer> econHttpClientHelper,
            ICustomerToEconomicCustomer customerToEconCustomer,
            ICustomerRepository customerRepository,
            IEconCusContactService econCusContactService,
            IEconPaymentTermsService econPaymentTermsService,
            IEconVatZoneService econVatZoneService,
            IEconDeliveryLocationService econDeliveryLocationService
            )
        {
            _httpClientHelper = httpClientHelper;
            _econHttpClientHelper = econHttpClientHelper;
            _customerToEconCustomer = customerToEconCustomer;
            _customerRepository = customerRepository;
            _econCusContactService = econCusContactService;
            _econPaymentTermsService = econPaymentTermsService;
            _econVatZoneService = econVatZoneService;
            _econDeliveryLocationService = econDeliveryLocationService;

        }
        public async Task<IEnumerable<EconCustomer>> ReadCustomerData()
        {
            var result = new List<EconCustomer>();
            int currentPage = 1;
             int totalPages = 0;

            do
            {
                var nextUrl = $"{EconApiHelper.baseUrl}customers?page={currentPage}";
                var response = await _httpClientHelper.GetAllItemsRequest(nextUrl, EconApiHelper.headerTokens);
                result.AddRange(response.collection);
                currentPage++;
                totalPages = response.pagination.resultsWithoutFilter/ response.pagination.pageSize;
            } while (currentPage <= totalPages );

            foreach (var item in result)
            {
                //get contacts for each data from corresponding api url
                var contacts = await _econCusContactService.GetAllCustomerContacts(item.contacts);
                item.econCustomerContacts = contacts.ToList();

                //Get Payment terms from url
                var econPaymentTerms = await _econPaymentTermsService.GetPaymentTerms(item.paymentTerms.self);
                item.paymentTerms = econPaymentTerms;

                //Get vat zone from url
                var vatzone =  await _econVatZoneService.GetVatZone(item.vatZone.self);
                item.vatZone = vatzone;

                // get shipping addresses from url
                var deliveryLocations = await _econDeliveryLocationService.GetAllDeliveryLocations(item.deliveryLocations);
                item.econDeliveryLocations = deliveryLocations.ToList();
            }
            return result;
        }

        public async Task PostOrUpdateData(List<Customer> customers)
        {

            //Convert customer to econCustomer
            var econCustomers = _customerToEconCustomer.ToEconCustomerList(customers);
            var apiCusList = await ReadCustomerData();

            var listToUpdate = econCustomers.Where(a => apiCusList.Any(b => b.customerNumber == a.customerNumber)).ToList();
            var listToAdd = econCustomers.Where(c => apiCusList.All(d => d.customerNumber != c.customerNumber)).ToList();

            //update or create payment terms

            foreach (var item in econCustomers)
            {
                var econPaymentTerms = await _econPaymentTermsService.GetOrCreateEconPaymentTerms(item.paymentTerms);
                item.paymentTerms = econPaymentTerms;

            }

            //post to econ api
            foreach (var item in listToAdd)
            {
               
                if (item != null)
                {

                    var econCustomer  = await PostEconCustomer(item);
                    item.customerNumber = econCustomer.customerNumber;

                    //assign customer number of newly created item to customer contacts
                    foreach (var cc in item.econCustomerContacts)
                    {
                        cc.customer.customerNumber = econCustomer.customerNumber;
                        cc.customer.self = "https://restapi.e-conomic.com/customers/" + item.customerNumber;
                    }

                    foreach (var dl in item.econDeliveryLocations)
                    {
                        dl.customer.customerNumber = item.customerNumber;
                        dl.customer.self = "https://restapi.e-conomic.com/customers/" + item.customerNumber;

                    }
                    // post customerContacts of the newly created item
                    if (item.econCustomerContacts.Count !=0)
                    {
                         await _econCusContactService.PostCustomerContacts(item.econCustomerContacts);

                    }

                    // post delivery locations of the newly created item
                    if (item.econDeliveryLocations.Count != 0)
                    {
                        await _econDeliveryLocationService.PostDeliveryLocations(item.econDeliveryLocations);

                    }



                    //update db row with new invoice ninja customer id
                    var dbCus = _customerRepository.Search(x => x.CustomerName == item.name).Result.FirstOrDefault();
                    dbCus.EconomicCustomerId = econCustomer.customerNumber;
                    await _customerRepository.UpdateFromClient(dbCus);
                }
            }

            //update Econ customer
            foreach (var item in listToUpdate)
            {
                var cc = await _econCusContactService.CUDCustomerContacts(item);
                item.econCustomerContacts = cc;
                await UpdateEconCustomer(item);
            }
        }
               

        private async Task<string> UpdateEconCustomer(EconCustomer customer)
        {
            customer.attention = new Attention()
            {
                customerContactNumber = customer.econCustomerContacts.Find(x => x.isPrimary == true).customerContactNumber,
                customer = new EconCustomer()
                {
                    customerNumber = customer.customerNumber,
                    self = customer.self,
                },
                self = "https://restapi.e-conomic.com/customers/" + customer.customerNumber + "/contacts/"
            };
            var url = $"{EconApiHelper.baseUrl}customers/{customer.customerNumber}";
            var json = JsonConvert.SerializeObject(customer);
            //var result = await _httpClientHelper.PutRequest(url, EconApiHelper.headerTokens, json);
            return await _econHttpClientHelper.PutRequest(url, EconApiHelper.headerTokens, json);
        }

        private async Task<EconCustomer> PostEconCustomer(EconCustomer item)
        {
            var url = $"{EconApiHelper.baseUrl}customers"; 
            var json = await _econHttpClientHelper.PostRequest(url, EconApiHelper.headerTokens, item);
            var obj = JsonConvert.DeserializeObject<EconCustomer>(json);
            return obj;
        }
    }
}
