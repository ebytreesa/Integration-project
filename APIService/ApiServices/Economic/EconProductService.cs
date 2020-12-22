using ConverterService.ProductConverter.Economic;
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

namespace APIService.ApiServices.Economic
{
    public class EconProductService : IEconProductService
    {

        private readonly IHttpClientHelper<EconProductsApiModel> _httpClientHelper;
        private readonly IHttpClientHelper<EconProduct> _productHttpClientHelper;

        private readonly IProductsToEconProducts _productsToEconProducts;
        private readonly IProductRepository _productRepository;
        public EconProductService(IHttpClientHelper<EconProductsApiModel> httpClientHelper,
            IProductsToEconProducts productsToEconProducts,
            IProductRepository productRepository,
            IHttpClientHelper<EconProduct> productHttpClientHelper
            )
        {
            _httpClientHelper = httpClientHelper;
            _productHttpClientHelper = productHttpClientHelper;
            _productsToEconProducts = productsToEconProducts;
            _productRepository = productRepository;

        }
        public async Task<List<EconProduct>> ReadProductData()
        {
            var result = new List<EconProduct>();
            int currentPage = 1;
            int totalPages = 0;

            do
            {
                var nextUrl = $"{EconApiHelper.baseUrl}products?page={currentPage}";
                var response = await _httpClientHelper.GetAllItemsRequest(nextUrl, EconApiHelper.headerTokens);
                result.AddRange(response.collection);
                currentPage++;
                totalPages = response.pagination.resultsWithoutFilter / response.pagination.pageSize;
            } while (currentPage <= totalPages);


            return result;
        }

        public async Task PostOrUpdateData(List<Product> products)
        {
            //Convert customer to econCustomer
            var econCustomers = _productsToEconProducts.ToEconProducts(products);
            var apiProductList = await ReadProductData();

            var listToUpdate = econCustomers.Where(a => apiProductList.Any(b => b.productNumber == a.productNumber)).ToList();
            var listToAdd = econCustomers.Where(c => apiProductList.All(d => d.productNumber != c.productNumber)).ToList();

            //post to econ api
            foreach (var item in listToAdd)
            {
                if (item != null)
                {
                    
                    var p = await PostEconProduct(item);
                    //update db row with new invoice ninja customer id
                    var dbProduct = _productRepository.Search(x => x.name == item.name).Result.FirstOrDefault();
                    dbProduct.econProductId = p.productNumber;
                    await _productRepository.Update(dbProduct);
                }
            }

            //update econ customer
            foreach (var item in listToUpdate)
            {
                // var existingCus = await GetCustomerByName(item.name);
                await UpdateEconProduct(item);
            }
        }

        private async Task<string> UpdateEconProduct(EconProduct product)
        {
            
            var url = $"{EconApiHelper.baseUrl}products/{product.productNumber}";
            var json = JsonConvert.SerializeObject(product);
            return await _productHttpClientHelper.PutRequest(url, EconApiHelper.headerTokens, json);
        }

        private async Task<EconProduct> PostEconProduct(EconProduct item)
        {
            var url = $"{EconApiHelper.baseUrl}products";
            var json = await _productHttpClientHelper.PostRequest(url, EconApiHelper.headerTokens, item);
            var obj = JsonConvert.DeserializeObject<EconProduct>(json);
            return obj;
        }
    }

}
