using ConverterService.ProductConverter.InvoiceNinja;
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

namespace APIService.ApiServices.InvoiceNinja
{
    public class NinjaProductService : INinjaProductService
    {
        private readonly IHttpClientHelper<NinjaProductApiModel> _httpClientHelper;
        private readonly IHttpClientHelper<NinjaProduct> _ninjaProducthttpClientHelper;
        private readonly IProductsToNinjaProducts _productsToNinjaProducts;
        private readonly IProductRepository _productRepository;
        public NinjaProductService(IHttpClientHelper<NinjaProductApiModel> httpClientHelper,
            IHttpClientHelper<NinjaProduct> ninjaProducthttpClientHelper,
            IProductsToNinjaProducts productsToNinjaProducts,
            IProductRepository productRepository
            )
        {
            _httpClientHelper = httpClientHelper;
            _ninjaProducthttpClientHelper = ninjaProducthttpClientHelper;
            _productsToNinjaProducts = productsToNinjaProducts;
            _productRepository = productRepository;
        }

        public async Task<List<NinjaProduct>> ReadProductData()
        {
            int currentPage = 1;
            int totalPages = 0;
            var result = new List<NinjaProduct>();

            do
            {
                var nextUrl = $"{NinjaApiHelper.baseUrl}products?page={currentPage}";
                //HttpClientHelper<NinjaCustomerApiModel> httpClientHelper = new HttpClientHelper<NinjaCustomerApiModel>();
                var resul = await _httpClientHelper.GetAllItemsRequest(nextUrl, NinjaApiHelper.headerTokens);
                totalPages = resul.meta.pagination.total_pages;
                currentPage++;
                result.AddRange(resul.data);

            } while (currentPage <= totalPages);

            return result.Where(x => x.is_deleted == false).ToList();
        }

        public async Task PostOrUpdateData(List<Product> products)
        {

            //Convert customer to ninjaCustomer
            var ninjaProducts = _productsToNinjaProducts.ToNinjaProducts(products);
            var apiProductList = await ReadProductData();

            var listToUpdate = ninjaProducts.Where(a => apiProductList.Any(b => b.id == a.id)).ToList();
            var listToAdd = ninjaProducts.Where(c => apiProductList.All(d => d.id != c.id)).ToList();


            //post to ninja api
            foreach (var item in listToAdd)
            {
                if (item != null)
                {
                    int id = await PostNinjaProduct(item);
                    //update db row with new invoice ninja customer id
                    var dbCus = _productRepository.Search(x => x.name == item.product_key).Result.FirstOrDefault();
                    dbCus.ninjaProductId = id;
                    await _productRepository.Update(dbCus);
                }
            }

            //update ninja customer
            foreach (var item in listToUpdate)
            {
                // var existingCus = await GetCustomerByName(item.name);
                await UpdateNinjaProduct(item);
            }
        }

        private async Task<string> UpdateNinjaProduct(NinjaProduct item)
        {
            var url = $"{NinjaApiHelper.baseUrl}products/{item.id}";
            var json = JsonConvert.SerializeObject(item);
            return await _ninjaProducthttpClientHelper.PutRequest(url, NinjaApiHelper.headerTokens, json);
        }

        private async Task<int> PostNinjaProduct(NinjaProduct item)
        {
            var url = $"{NinjaApiHelper.baseUrl}products"; ;
            var json = await _ninjaProducthttpClientHelper.PostRequest(url, NinjaApiHelper.headerTokens, item);
            var obj = JsonConvert.DeserializeObject<NinjaProductData>(json);
            return obj.data.id;

        }
    }


}
