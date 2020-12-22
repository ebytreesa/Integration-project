using Domain.Models;
using Economic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConverterService.ProductConverter.Economic
{
    public class EconProductsToProducts : IEconProductsToProducts
    {
        public List<Product> ToProductList(List<EconProduct> econProducts)
        {
            var products = new List<Product>();
            foreach (var item in econProducts)
            {
                products.Add(new Product
                {
                    name = item.name,
                    econProductId = item.productNumber,
                    description = "",
                    recommendedPrice = item.recommendedPrice,
                    salesPrice = item.salesPrice,
                    lastUpdated = item.lastUpdated,
                    dbUpdatedAt = DateTime.Now,
                    source = "E-conomic",
                    sourceId = item.productNumber
                });

            }
            return products;


        }

    }
}
